local spritesFolder = app.params["sprites-folder"]

if not spritesFolder then
  spritesFolder = "Sprites/"
end

local outputPath = spritesFolder .. app.fs.fileTitle(app.activeSprite.filename)
app.fs.makeDirectory(outputPath)

local sliceCount = {}
for _, slice in ipairs(app.activeSprite.slices) do
    local sliceId = slice.data .. "/" .. slice.name
    sliceCount[sliceId] = (sliceCount[sliceId] or 0) + 1
end

for _, slice in ipairs(app.activeSprite.slices) do
    app.activeSprite:crop(slice.bounds)

    local sliceId = slice.data .. "/" .. slice.name
    local sliceFilename = slice.name .. ".png"

    if sliceCount[sliceId] > 1 then
        sliceFilename = sliceFilename .. "_" .. sliceCount[sliceId]
    end

    local groupPath = slice.data:gsub("[^%w_]", "/")
    local fullOutputPath = app.fs.joinPath(outputPath, groupPath)

    app.fs.makeDirectory(fullOutputPath)
    app.activeSprite:saveCopyAs(app.fs.joinPath(fullOutputPath, sliceFilename))
    sliceCount[sliceId] = sliceCount[sliceId] - 1
end

return 0