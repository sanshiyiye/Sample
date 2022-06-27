---@type BillboardData
local BillboardData = {}

function BillboardData.Get (key)
    return {
        Title = "Title",
        Content = key .."--data"
    }
end

return BillboardData