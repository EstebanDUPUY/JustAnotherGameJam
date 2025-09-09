local sprite = app.activeSprite
local trim = app.params["trim"]
local trimCels = app.params["trim-cels"]
local spritesFolder = app.params["sprites-folder"]
print(spritesFolder)

if not spritesFolder then
  spritesFolder = "Sprites/"
end

if not trim then
  trim = true
end

if trim == "false" then
  trim = false
end

if trimCels == "true" then
  trimCels = true
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

for _, group in ipairs(sprite.layers) do
  if group.isGroup then
    local groupFolderPath = app.fs.joinPath(baseFilename .. "/" .. group.name)
    app.fs.makeDirectory(groupFolderPath)

    for _, tag in ipairs(sprite.tags) do
      local sanitizedTagName = tag.name:gsub("[^%w_]", "")
      local outputFilename = app.fs.joinPath(groupFolderPath, sanitizedTagName .. ".png")

      app.command.ExportSpriteSheet {
        ui = false,
        askOverwrite = false,
        tag = tag.name,
        layer = group.name,
        type = SpriteSheetType.HORIZONTAL,
        textureFilename = outputFilename,
        splitTags = true,
        trimSprite = true,
        innerPadding = 1
      }
    end
  end
end

local function has_groups(sprite)
  for _, layer in ipairs(sprite.layers) do
    if layer.isGroup then
      return true
    end
  end
  return false
end

if not has_groups(sprite) then

  for _, tag in ipairs(sprite.tags) do
    local sanitizedTagName = tag.name:gsub("[^%w_]", "")
    local outputFilename = app.fs.joinPath(baseFilename, sanitizedTagName .. ".png")

    app.command.ExportSpriteSheet {
      ui = false,
      askOverwrite = false,
      tag = tag.name,
      type = SpriteSheetType.HORIZONTAL,
      textureFilename = outputFilename,
      splitTags = true,
      trimSprite = trim
    }
  end
end