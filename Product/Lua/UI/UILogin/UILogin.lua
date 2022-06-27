---@type UILogin
local UILogin = {}
extends(UILogin, UIBase)

function UILogin.New(controller)
    local newUILogin = new(UILogin)
    newUILogin.Controller = controller
    return newUILogin
end

---测试场景数组
local scenes = { "Scene/Scene1001/Scene1001", "Scene/Scene1002/Scene1002" }

-- controller also pass to OnInit function
function UILogin:OnInit(controller)
    self.sceneIndex = 1
    print("================================ UILogin:OnInit ============================")

    ---多语言示例，从语言表中读取
    -- self.LoginText from LuaOutlet
    --self.LoginText.text = I18N.Str("UILogin.LoginDescText")

    Tools.SetButton(self.btnSwithScene, function()
        self.sceneIndex = self.sceneIndex == 1 and 2 or 1
        -- SceneLoader.Load(scenes[self.sceneIndex], function(isOK)
        --     if not isOK then
        --         return print("SceneLoader.Load faild")
        --     end
        --     print("switch scene success")
        -- end, LoaderMode.Async)
    end)
    Tools.SetButton(self.btnBillboard, function()
        print('Click the button!!!')
        UIModule.getInstance():OpenWindow("UIBillboard")
    end)

    Tools.SetButton(self.btnSwithUI, function()
        UIModule.getInstance():CloseWindow("UILogin")
        UIModule.getInstance():OpenWindow("Main", "user1")
    end)
    Tools.SetButton(self.btnLoadSprite, function()
        print("================================btnLoadSprite:Click ============================ ")
        UIModule.getInstance():OpenWindow("UILoading")
    end)
    Tools.SetButton(self.btnLoadLoading, function()
        print("================================btnLoadLoading:Click ============================ ")
        UIModule.getInstance():OpenWindow("UILoadUISprite")
    end)
    
end

function UILogin:OnOpen(num1)
    print("================================ UILogin:OnOpen ============================, arg1: " .. tostring(num1))
    UIModule.getInstance():OpenWindow("UINavbar")
    UIModule.getInstance():OpenWindow("Main", 888);
    print(self);
end

function UILogin:OnClose()
    print(string.format("btnBillboard=%s ,sceneIndex=%s",self.btnBillboard,self.sceneIndex))
end

return UILogin
