using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell // 0 - North, 1 - South, 2 - East, 3 - West
    {
        public bool visited = false;
        public bool[] status = new bool[4];
        public bool HasKey = false;
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligatory;

        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot spawn, 1 - can spawn, 2 - HAS to spawn

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }

    }

    public GameObject enemy;

    //Initialize from Data Manager
    public int seed;

    //Maximum number of enemies per room
    private int maxNumEnemies = 4;

    //Chest for Key
    public GameObject chest;

    //size of dungeon in 2D
    public Vector2Int size;

    //start position of dungeon
    public int startPos = 0;

    public Rule[] rooms;

    public GameObject[] corridorsObjects;

    //The distance between each room
    public Vector2 offset;

    //Our board of dungeon rooms for the DFS algorithm to work
    List<Cell> board;

    //Our board of dungeon corridors
    Dictionary<(int, int), bool> corridors;

    //Data list for enemies
    Dictionary<int, int> enemiesPerRoom = new Dictionary<int, int>();

    //Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        board = new List<Cell>();
        corridors = new Dictionary<(int prevRoom, int nextRoom), bool>();

        //Add all the cells that the algorithm must have
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        //Define the new seed
        Random.seed = PlayerProfile.gameData.currentArea.GetPlayerSeed();
        //Random.seed = 0;

        //Start the DFS for the 1st time
        MazeGenerator();

        while (board.Exists(cell => !cell.visited))
        {
            //Find a second position at random to start the algorithm again
            List<int> positions = new List<int>();
            foreach (Cell cell in board)
            {
                int pos = board.IndexOf(cell);
                if (cell.visited && (pos != (board.Count - 1)) && CheckNeighbors(pos).Count != 0)
                {
                    positions.Add(pos);
                }
            }
            startPos = positions[Random.Range(0, positions.Count)];

            //Start the DFS for the nth time
            MazeGenerator();
        }

        //Find the furthest room to add the key
        board[FindFurthestRoom()].HasKey = true;

        //Generate the dungeon
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        List<EnemyData> enemies= new List<EnemyData>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                        {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else
                        {
                            randomRoom = 0;
                        }
                    }

                    GameObject gameRoom;
                    RoomBehaviour newRoom;

                    if (board[(i + j * size.x)].HasKey)
                    {
                        // Chest room has no enemies
                        gameRoom = Instantiate(chest, new Vector3(2 * i * offset.x, 0, 2 * -j * offset.y), Quaternion.identity, transform);
                        newRoom = gameRoom.GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name = "Room " + i + "-" + j;
                    }
                    else
                    {
                        gameRoom = Instantiate(rooms[randomRoom].room, new Vector3(2 * i * offset.x, 0, 2 * -j * offset.y), Quaternion.identity, transform);
                        newRoom = gameRoom.GetComponent<RoomBehaviour>();

                        Vector3 position = gameRoom.transform.position;
                        
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name = "Room " + i + "-" + j;

                        // On the last room or on the start room
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }
                        else if (i == size.x - 1 && j == size.y - 1)
                        {
                            enemies.Add(new EnemyData("Boss" + enemies.Count, 200, 1, position));
                        }
                        else
                        {
                            int numberOfEnemies = Random.Range(1, maxNumEnemies);
                            for (int k = 0; k <= numberOfEnemies; k++)
                            {
                                enemies.Add(new EnemyData("Enemy" + enemies.Count + k, 100, 0, position));
                            }
                        }
                    }
                }
            }
        }
        
        foreach (var tuple in corridors.Keys)
        {
            int rowStart = tuple.Item1 / size.x;
            int colStart = tuple.Item1 % size.x;

            int rowEnd = tuple.Item2 / size.x;
            int colEnd = tuple.Item2 % size.x;

            GameObject corridor = corridorsObjects[Random.Range(0, corridorsObjects.Count())];

            if (corridors[tuple])
            {
                //Horizontal
                var newCorridor = Instantiate(corridor, new Vector3(Mathf.Abs((colStart * offset.x * 2) + (colEnd * offset.x * 2)) / 2, 0, -rowStart * 2 * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newCorridor.name = "Corridor " + tuple.Item1 + "->" + tuple.Item2;
            }
            else
            {
                //Vertical
                var newCorridor = Instantiate(corridor, new Vector3(2 * colStart * offset.x, 0, -Mathf.Abs((rowStart * offset.x * 2) + (rowEnd * offset.x * 2)) / 2), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                newCorridor.name = "Corridor " + tuple.Item1 + "->" + tuple.Item2;
                newCorridor.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            }
        }
        PlayerProfile.SetCurrentAreaEnemyList(enemies);
        GameObject.Find("GameManager").GetComponent<GameRuntimeManager>().Spawn();
    }

    void MazeGenerator()
    {
        //Keeps the position that we are now
        int currentCell = startPos;

        //Calling path
        Stack<int> path = new Stack<int>();

        int k = 0;

        //Show us the limit of the dungeon to be sure to exit. we can define it also as while(true)
        int limit = 1000;

        while (k < limit)
        {
            k++;

            //We are sure that we always check the current node
            board[currentCell].visited = true;

            //Found the end
            if (currentCell == board.Count - 1)
            {
                break;
            }

            //Check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            //No available neighbors
            if (neighbors.Count == 0)
            {
                //We reached the last cell on the path
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    //There are more cells on the path
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                //Choose a neighbor at random
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //south or east
                    if (newCell - 1 == currentCell)
                    {
                        //East
                        corridors.Add((currentCell, newCell), true);
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        //South
                        corridors.Add((currentCell, newCell), false);
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //north or west
                    if (newCell + 1 == currentCell)
                    {
                        //West
                        corridors.Add((currentCell, newCell), true);
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        //North
                        corridors.Add((currentCell, newCell), false);
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        // Set the North side of Starting room to true
        board[0].status[0] = true;
    }

    int FindFurthestRoom()
    {
        int currentRoom = 0;

        Dictionary<int, int> rooms = new Dictionary<int, int>();
        int steps = 0;

        //Calling path
        Stack<int> path = new Stack<int>();

        //Add the first room as step = 0
        rooms.Add(currentRoom, steps);

        while (rooms.Count < board.Count)
        {
            //Check the other rooms/passages
            //Find the nextRoom where the value is true for the specified prevRoom and not visited -> not in the rooms dictionary
            int firstNextRoom = corridors
                .Where(entry => entry.Key.Item1 == currentRoom && !rooms.ContainsKey(entry.Key.Item2))
                .Select(entry => entry.Key.Item2).FirstOrDefault();

            if ((firstNextRoom == 0) && !(path.Count == 0))
            {
                currentRoom = path.Pop();
                steps -= 1;
            }
            else
            {
                path.Push(currentRoom);
                currentRoom = firstNextRoom;
                steps += 1;

                //Add the room to the dictionary
                rooms.Add(currentRoom, steps);
            }
        }

        // Remove the last room
        rooms.Remove(board.Count - 1);

        return rooms.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
    }

    private void SpawnEnemies(GameObject room, int numberOfEnemies)
    {
        for (int i = 1; i <= numberOfEnemies; i++)
        {
            enemy.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Instantiate(enemy, room.transform);
        }
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check north neighbor
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            neighbors.Add((cell - size.x));
        }

        //check south neighbor
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        //check east neighbor
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        //check west neighbor
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }
}
