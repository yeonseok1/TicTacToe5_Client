
using UnityEngine;

public static class TicTacToeAI
{
    // 현재 상태를 전달하면 다음 최적의 수를 반환
    public static (int row, int col)? GetBestMove(Constants.PlayerType[,] board)
    {
        float bestScore = -1000;
        (int row, int col) movePosition = (-1, -1);

        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == Constants.PlayerType.None)
                {
                    board[row, col] = Constants.PlayerType.PlayerB;
                    var score = DoMinMax(board, 0, false);
                    board[row, col] = Constants.PlayerType.None;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        movePosition = (row, col);
                    }
                }
            }
        }

        if (movePosition != (-1, -1))
        {
            return (movePosition.row, movePosition.col);
        }

        return null;
    }

    private static float DoMinMax(Constants.PlayerType[,] board, int depth, bool isMaximizing)
    {
        if (GameLogic.CheckGameWin(Constants.PlayerType.PlayerA, board))
            return -10 + depth;
        if (GameLogic.CheckGameWin(Constants.PlayerType.PlayerB, board))
            return 10 - depth;
        if (GameLogic.CheckGameDraw(board))
            return 0;

        if (isMaximizing)
        {
            var bestScore = float.MinValue;
            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == Constants.PlayerType.None)
                    {
                        board[row, col] = Constants.PlayerType.PlayerB;
                        var score = DoMinMax(board, depth + 1, false);
                        board[row, col] = Constants.PlayerType.None;
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            var bestScore = float.MaxValue;
            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == Constants.PlayerType.None)
                    {
                        board[row, col] = Constants.PlayerType.PlayerA;
                        var score = DoMinMax(board, depth + 1, true);
                        board[row, col] = Constants.PlayerType.None;
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }

    }

}
