------AutoLuaGenerator Start----------------------
local UIBase = import('UI/UIBase')
local BillboardData = import('UI/UIBillboard/BillboardData')
---@type UIBillboard
local UIBillboard = {}

extends(UIBillboard, UIBase)
------AutoLuaGenerator End-----------------------
function UIBillboard:OnInit(controller)
    print("================================ UIBillboard:OnInit ============================")
    self.Controller = controller
    if not self.txtTitle then
        self.txtTitle = self:GetUIText('txtTitle')
    end
    if not self.txtContent then
        self.txtContent = self:GetUIText('txtContent')
    end
    Tools.SetButton(self.btn_close, function()
        print('Click the btn_close!!!')
        UIModule.getInstance():CloseWindow("UIBillboard")
        UIModule.getInstance():OpenWindow("UILogin")

    end)

end

function UIBillboard:OnOpen()
    print("================================ UIBillboard:OnOpen ============================")
    local rand
    --rand = Cookie.Get('UIBillboard.RandomNumber')
    --if not rand then
    rand = math.random(1, 3)
    --    Cookie.Set('UIBillboard.RandomNumber', rand)
    --end


    local billboardSetting = BillboardData.Get('Billboard' .. tostring(rand))

    self.txtTitle.text = billboardSetting.Title
    self.txtContent.text = billboardSetting.Content

    --尝试从Billboard.lua中读取配置数据
    print("from lua config ,id:",billboardSetting.Id)
end

function UIBillboard:OnClose()
    print("================================ UIBillboard:OnClose ============================")
end
return UIBillboard
