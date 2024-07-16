using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SpriteMaterialPropertyBlock<T> : MonoBehaviour
{
    [SerializeField]
    protected List<MaterialPropertiesBlockData<T>> materialPropertiesBlockDatas =
        new List<MaterialPropertiesBlockData<T>>();
    private void Awake()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (var propertyBlock in materialPropertiesBlockDatas.Select(SetMaterialProperties))
        {
            spriteRenderer.SetPropertyBlock(propertyBlock);
        }
    }

    protected abstract MaterialPropertyBlock SetMaterialProperties(MaterialPropertiesBlockData<T> materialPropertiesBlockData);
}

[Serializable]
public struct MaterialPropertiesBlockData<T>
{
    public T materialPropertyValue;
    public string materialPropertyName;
}
