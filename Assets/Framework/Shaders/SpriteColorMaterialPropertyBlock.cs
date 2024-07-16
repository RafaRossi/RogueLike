using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorMaterialPropertyBlock : SpriteMaterialPropertyBlock<Color>
{
    protected override MaterialPropertyBlock SetMaterialProperties(MaterialPropertiesBlockData<Color> materialPropertiesBlockData)
    {
        var materialPropertyBlock = new MaterialPropertyBlock();
        
        materialPropertyBlock.SetColor(materialPropertiesBlockData.materialPropertyName, materialPropertiesBlockData.materialPropertyValue);

        return materialPropertyBlock;
    }
}
