local sprite = app.activeSprite
local spritesFolder = app.params["sprites-folder"]

if not spritesFolder then
  spritesFolder = "Sprites/"
end

if not sprite then
  return app.alert("No active sprite")
end

local baseFolder = spritesFolder
local baseFilename = baseFolder .. sprite.filename

if baseFilename == "" or baseFilename == nil then
  baseFilename = baseFolder
else
  baseFilename = baseFolder .. app.fs.fileTitle(baseFilename)
end


local outputFilename = app.fs.joinPath(baseFilename, app.fs.fileTitle(sprite.filename) .. ".png")
print(outputFilename)

app.command.ExportSpriteSheet {
    ui = false,
    askOverwrite = false,
    type = SpriteSheetType.HORIZONTAL,
    textureFilename = outputFilename,
    splitTags = true,
    trimSprite = true,
}