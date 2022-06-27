using System.Collections;
using System.Linq;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace MegaDad
{
    public class OverheadMegaDadController : MonoBehaviour
    {
        private enum State
        {
            WaitingForInput,
            Moving,
            Drowning
        }

        private State m_State;

        private const float MovementBlockSize = 8.0f;
        private const float MovingSpeedPPS = 64.0f;
        private const float TimeToMoveOneBlock = MovementBlockSize / MovingSpeedPPS;

        public GameObject m_SplashPrefab;
        public Tilemap m_tilemap;
        public Button[] Buttons;

        private Vector2 m_Facing = Vector2.down;
        private Animator m_Animator;

        private float m_MoveTimer;
        private Vector2 m_MovingFrom;
        private Vector2 m_MovingTo;
        private Vector2 m_SpawnPoint;

        private SpriteRenderer m_Renderer;

        private void Awake()
        {
            m_Animator = gameObject.GetComponentInChildren<Animator>();
            m_Animator.SetFloat("Dir_x", m_Facing.x);
            m_Animator.SetFloat("Dir_y", m_Facing.y);

            m_Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            SetOverheadCamera();

            // Find the position on the game map we're supposed to spawn at
            var spawner = FindObjectsOfType<SuperTiled2Unity.SuperObject>().FirstOrDefault(s => s.m_TiledName == "Spawn");
            if (spawner != null)
            {
                m_SpawnPoint = spawner.transform.position;
            }

            // Make sure the player starts off aligned to the (imaginary) grid
            m_SpawnPoint.x = RoundToGrid(m_SpawnPoint.x);
            m_SpawnPoint.y = RoundToGrid(m_SpawnPoint.y);

            gameObject.transform.localPosition = m_SpawnPoint;

            m_State = State.WaitingForInput;

            changeTileInRunning();
            btnCtrl();
        }

        private void btnCtrl()
        {
            Buttons = FindObjectsOfType<Button>();
            foreach (Button button in Buttons)
            {
                button.onClick.AddListener(() => { onClickBtn(button);});
            }
        }

        private void onClickBtn(Button item)
        {
            switch (item.name)
            {
                case "upBtn":
                    m_Animator.SetBool("Moving", true);
                    break;
                default:
                    break;
            }
        }

        private void changeTileInRunning()
        {
            var _tile = m_tilemap.GetTile(new Vector3Int(13, 0, 0));
            
            m_tilemap.SetTile(new Vector3Int(13,-3,0), _tile);
            m_tilemap.SetTile(new Vector3Int(13,-4,0), _tile);
            m_tilemap.SetTile(new Vector3Int(13,-5,0), _tile);
        }

        private void Update()
        {
            // mouseInputUpdate();
            // touchInPhone();
            if (m_State == State.Moving)
            {
                MoveUpdate();
            }

            if (m_State == State.WaitingForInput)
            {
                InputUpdate();
            }
            // touchInPhone();
        }

        private void mouseInputUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
                transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            }
        }

        private void touchInPhone()
        {
            float sp = 0.1f;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                transform.Translate(touchDeltaPosition.x * sp, touchDeltaPosition.y * sp, 0);
            }
        }

        private void LateUpdate()
        {
            m_Animator.SetFloat("Dir_x", m_Facing.x);
            m_Animator.SetFloat("Dir_y", m_Facing.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                StartCoroutine(Drowned());
            }
        }

        private void MoveUpdate()
        {
            // Move towards our target position
            m_MoveTimer += Time.deltaTime;

            var ratio = m_MoveTimer / TimeToMoveOneBlock;

            var pos = Vector2.Lerp(m_MovingFrom, m_MovingTo, ratio);
            gameObject.transform.position = pos;

            if (ratio >= 1.0f)
            {
                m_State = State.WaitingForInput;
                m_MoveTimer = Mathf.Repeat(m_MoveTimer, TimeToMoveOneBlock);
            }
        }

        private void InputUpdate()
        {
            Vector2 dv = Vector2.zero;
            dv.x = Input.GetAxisRaw("Horizontal");
            dv.y = Input.GetAxisRaw("Vertical");
            // Favor horizontal movement over vertical
            if (dv.x != 0)
            {
                m_Facing.x = dv.x;
                m_Facing.y = 0;
            }
            else if (dv.y != 0)
            {
                m_Facing.x = 0;
                m_Facing.y = dv.y;
            }

            m_Facing.Normalize();
            m_MovingFrom = gameObject.transform.position;

            if (dv.SqrMagnitude() > 0 && !Input.GetKey(KeyCode.LeftControl))
            {
                // We are attempting to move so we want to animate
                m_Animator.SetBool("Moving", true);

                // We may not be allowed to move, however, if that would cause a collision with the default colliders
                var pos = gameObject.transform.position;
                var hit = Physics2D.Raycast(pos, m_Facing, MovementBlockSize, 1 << LayerMask.NameToLayer("Default"));

                if (hit)
                {
                    m_State = State.WaitingForInput;
                    m_MoveTimer = 0.0f;
                }
                else
                {
                    m_State = State.Moving;
                    m_MovingTo = m_MovingFrom + m_Facing * MovementBlockSize;
                }
            }
            else
            {
                // No input means we aren't even trying to move
                m_MoveTimer = 0.0f;
                m_Animator.SetBool("Moving", false);
            }
        }

        private int RoundToGrid(float value)
        {
            if (value < 0)
            {
                return (int)(((value - MovementBlockSize * 0.5f) / MovementBlockSize)) * (int)MovementBlockSize;
            }

            return (int)(((value + MovementBlockSize * 0.5f) / MovementBlockSize)) * (int)MovementBlockSize;
        }

        private IEnumerator Drowned()
        {
            m_State = State.Drowning;

            // Place the player in the water
            var pos = m_MovingTo + m_Facing * MovementBlockSize;

            // Move the player into the water
            gameObject.transform.position = pos;
            m_MoveTimer = 0.0f;
            yield return new WaitForSeconds(0.0675f);

            // Move the player to the spawn location and make him invisible
            gameObject.transform.position = m_SpawnPoint;
            m_Facing = Vector2.down;
            m_Animator.SetBool("Moving", false);
            m_Renderer.enabled = false;

            // Spawn a splash prefab where we fell into the water
            var splash = Instantiate(m_SplashPrefab, pos, Quaternion.identity);
            Destroy(splash, 0.5f);

            yield return new WaitForSeconds(0.125f);

            for (int i = 0; i < 8; i++)
            {
                m_Renderer.enabled = !m_Renderer.enabled;
                yield return new WaitForSeconds(0.125f);
            }

            // We're done. Go back to waiting for input.
            m_Renderer.enabled = true;
            m_State = State.WaitingForInput;
        }

        private void SetOverheadCamera()
        {
            // This example requires a sort axis for sorting
            // (This could be set globablly for all cameras in the project settings)
            var camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            camera.transparencySortMode = TransparencySortMode.CustomAxis;
            camera.transparencySortAxis = Vector3.up;
        }
    }
}