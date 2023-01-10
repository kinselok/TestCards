using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public interface IGameFactory
    {
        GameObject CreateHorizontalBlock();
        GameObject CreateVerticalBlock();
    }
}