using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Block[] blocks;

    public delegate void OnBlockClicked(int row, int col);
    public OnBlockClicked OnBlockClickedDelegate;

    // 1. Block �ʱ�ȭ

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

    // 2. Blcok ��Ŀ ǥ��

    public void PlaceMarker(Block.MarkerType markerType, int row, int col)
    {
        var blockIndex = row * Constants.blockColumnCount + col;
        blocks[blockIndex].SetMarker(markerType);
    }

    // 3. Block�� ���� ����
    public void SetBlcokColor()
    {
        // TODO: ���� ������ �ϼ��Ǹ� ����
    }

}
