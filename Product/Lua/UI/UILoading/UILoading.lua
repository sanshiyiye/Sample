------AutoLuaGenerator Start----------------------
local UIBase = import('UI/UIBase')
---@type UILoading
local UILoading = {}
extends(UILoading, UIBase)

-- create a ui instance
function UILoading.New(controller)
    local newUI = new(UILoading)
    newUI.Controller = controller
    return newUI
end
------AutoLuaGenerator End-----------------------
function UILoading:OnInit(controller)
        print('UILoading OnInit, do controls binding')
end

function UILoading:OnOpen(num)
        local Image = self.Image
        ResourceModule.Instance:LoadSprite("uiatlas/atlas_common", "btn_win_close", function(isOk, sprite)
                if (isOk) then
                        self.Image.sprite = sprite;
                        self.Image:SetNativeSize();
                    print("图片从图集加载完成");
                end
            end);
        print('UILoading OnOpen, do your logic')
end

return UILoading
