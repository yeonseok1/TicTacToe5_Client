using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked OnBlockClickedDelegate;

    // 1. Block 초기화

    public void InitBlocks()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].InitMarker(i, blockIndex =>
            {
                var row = blockIndex / Constants.blockColumnCount;
                var col = blockIndex % Constants.blockColumnCount;
                OnBlockClickedDelegate?.Invoke(row, col);
            });
        }
    }

    // 2. Blcok 마커 표시

    public void PlaceMarker(Block.MarkerType markerType, int row, int col)
    {
        var blockIndex = row * Constants.blockColumnCount + col;
        blocks[blockIndex].SetMarker(markerType);
    }

    // 3. Block의 배경색 설정
    public void SetBlcokColor()
    {
        // TODO: 게임 로직이 완성되면 구현
    }

}
