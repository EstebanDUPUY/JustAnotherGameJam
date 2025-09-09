local Library = {}

function Library.isFrameEmpty(sprite, frame)
  for i, cel in ipairs(sprite.cels) do
    if cel.frameNumber == frame.frameNumber then
      print(cel.image:isEmpty())
      if cel.image and cel.image:isEmpty() == false then
        return false
      end
    end
  end

  return true
end

function Library.isTagEmpty(sprite, tag)
  for i = tag.fromFrame.frameNumber, tag.toFrame.frameNumber do
    local frame = sprite.frames[i]
    
    if not Library.isFrameEmpty(sprite, frame) then
      return false
    end
  end

  return true
end

function Library.isLayerFrameEmpty(sprite, frame, layer)
  if not layer.isGroup then
    for i, cel in ipairs(sprite.cels) do
      if cel.frameNumber == frame.frameNumber and cel.layer == layer then
        if cel.image and not cel.image:isEmpty() then
          return false
        end
      end
    end
    return true
  else
    for _, subLayer in ipairs(layer.layers) do
      if not Library.isLayerFrameEmpty(sprite, frame, subLayer) then
        return false
      end
    end
    return true
  end
end

function Library.isLayerTagEmpty(sprite, tag, layer)
  for i = tag.fromFrame.frameNumber, tag.toFrame.frameNumber do
    local frame = sprite.frames[i]

    if not Library.isLayerFrameEmpty(sprite, frame, layer) then
      return false
    end
  end

  return true
end


return Library