
local UIBase = import('UI/UIBase')
---@type Main
local Main = {}
extends(Main, UIBase)

-- create a ui instance
function Main.New(controller)
    local newUI = new(Main)
    newUI.Controller = controller
    return newUI
end

function Main:OnInit(controller)
   
    Log.Info(8,"Main:OnInit")
    --TODO position 等调用统一处理
    -- Log.Info(8,self.Image.localPosition.x)
    -- print( UIModule.getInstance().OpenWindow)
    -- UIModule.getInstance():OpenWindow("UILogin")
    
end

function Main:OnOpen()
    print('Main OnOpen, do your logic')
end

return Main
