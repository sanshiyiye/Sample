---@type UILoadUISprite
local UILoadUISprite = {}
extends(UILoadUISprite, UIBase)

-- create a ui instance
function UILoadUISprite.New(controller)
    local newUI = new(UILoadUISprite)
    newUI.Controller = controller
    return newUI
end

function UILoadUISprite:OnInit(controller)
    print('UILoadUISprite OnInit, do controls binding')
end

function UILoadUISprite:OnOpen()
    
    ResourceModule.Instance:LoadUIAssets("UIHead",function(isOk, obj)
        if (isOk) then
            -- 仅仅是示例 此处不应该如此写
            obj.transform.parent = self.transform;
            print("图片从图集加载完成");
        end
    end);
end

return UILoadUISprite
