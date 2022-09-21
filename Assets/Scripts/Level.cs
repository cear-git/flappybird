using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float cameraSize = 50f;
    private const float pipeWidth = 7.8f;
    private const float pipeHeadHeight = 3.75f;
    private const float pipeSpeed = 30f;
    private const float pipeDestroyPos = -120f;
    private const float pipeSpawnPos = +120f;
    private const float birdPos = 0f;

    private static Level instance;
    public static Level getInstance() { return instance; }

    private List<Pipe> pipeList;
    private int pipesPassed;
    private int pipeIndex;
    private float pipeSpawnTimer;
    private float pipeSpawnMax;
    private float gapSize;
    private State state;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        VeryHard
    }
    public enum State
    {
        Waiting,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        pipeList = new List<Pipe>();
        state = State.Waiting;
        setDiff(Difficulty.Easy);
    }
    private void Start()
    {
        Bird.getInstance().onDied += _onDied;
        Bird.getInstance().onStart += _onStart;
    }

    private void _onDied(object sender, System.EventArgs e)
    {
        state = State.Dead;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
    private void _onStart(object sender, System.EventArgs e)
    {
        state = State.Playing;
    }
    
    private void Update()
    {
        if (state == State.Playing) {
            handleMovement();
            handlePipeSpawns();
        }
    }

    private void handlePipeSpawns()
    {
        pipeSpawnTimer -= Time.deltaTime;
        if (pipeSpawnTimer < 0)
        {
            pipeSpawnTimer = pipeSpawnMax;

            float heightEdge = 10f;
            float minHeight = gapSize * .5f + heightEdge;
            float maxHeight = (cameraSize * 2f) - gapSize * .5f - heightEdge;
            float height = Random.Range(minHeight, maxHeight);

            createGap(height, gapSize, pipeSpawnPos);
        }
    }
    private void handleMovement()
    {
        for (int i = 0; i < pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            bool toRight = pipe.getPos() > birdPos;
            pipe.Move();
            if (toRight && pipe.getPos() <= birdPos && pipe.isBottom())
            {
                pipesPassed++;
            }
            if (pipe.getPos() < pipeDestroyPos) { 
                //destroy pipe and remove, minus an index so that it doesnt skip an index after being removed
                pipe.destroySelf();
                pipeList.Remove(pipe);
                i--;
            }
        }
    }

    public int getPipesPassed()
    {
        return pipesPassed;
    }

    private void setDiff(Difficulty diff)
    {
        switch (diff)
        {
            case Difficulty.Easy:
                gapSize = 50f;
                pipeSpawnMax = 1.2f;
                break;
            case Difficulty.Medium:
                gapSize = 40f;
                pipeSpawnMax = 1.1f;
                break;
            case Difficulty.Hard:
                gapSize = 34f;
                pipeSpawnMax = 1f;
                break;
            case Difficulty.VeryHard:
                gapSize = 25f;
                pipeSpawnMax = .9f;
                break;
        }
    }

    private Difficulty getDiff()
    {
        switch (pipeIndex)
        {
            case >= 80:
                Debug.Log("vhard");
                return Difficulty.VeryHard;
            case >= 40:
                Debug.Log("hard");
                return Difficulty.Hard;
            case >= 20:
                Debug.Log("medium");
                return Difficulty.Medium;
            default:
                return Difficulty.Easy;
        }
    }

    private void createGap(float y, float size, float pos)
    {
        createPipe(y - size * .5f, pos, true);
        createPipe(cameraSize * 2f - y - size * .5f, pos, false);
        pipeIndex++;
        Debug.Log(pipeIndex.ToString());
        setDiff(getDiff());
    }

    private void createPipe(float height, float pos, bool bottom)
    {
        //set up the pipe head
        Transform pipeHead = Instantiate(GameAssets.getInstance().pfPipeHead);
        float pipeHeadY;
        pipeHeadY = bottom ? -cameraSize + height - pipeHeadHeight * .5f : +cameraSize - height + pipeHeadHeight * .5f;
        pipeHead.position = new Vector3(pos, pipeHeadY);

        //set up the pipe body
        Transform pipeBody = Instantiate(GameAssets.getInstance().pfPipeBody);
        float pipeBodyY;
        pipeBodyY = bottom ? -cameraSize : +cameraSize;
        if (!bottom) { pipeBody.localScale = new Vector3(1, -1, 1); }

        pipeBody.position = new Vector3(pos, pipeBodyY);

        //change the size of the pipe body
        SpriteRenderer pipeBodySR = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySR.size = new Vector2(pipeWidth, height);

        //change the box collider to match the pipe
        BoxCollider2D pipeBodyBC = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBC.size = new Vector2(pipeWidth, height);
        pipeBodyBC.offset = new Vector2(0f, height * 0.5f);

        //add the pipe head and body to a pipe class and add it to the list
        Pipe pipe = new Pipe(pipeHead, pipeBody, bottom);
        pipeList.Add(pipe);
    }

    private class Pipe
    {
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;
        private bool bottom;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform, bool isBottom)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
            this.bottom = isBottom;
        }   

        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * pipeSpeed * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * pipeSpeed * Time.deltaTime;
        }

        public float getPos()
        {
            return pipeHeadTransform.position.x;
        }

        public bool isBottom()
        {
            return bottom;
        }
        public void destroySelf()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }

}


