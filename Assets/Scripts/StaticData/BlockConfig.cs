using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

namespace Scripts.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/BlockConfig", fileName = "BlockConfig", order = 0)]
    public class BlockConfig : ScriptableObject
    {
        public BlockType Type;
        public List<BlockElementsConfig> ElementConfigs;
    }
}