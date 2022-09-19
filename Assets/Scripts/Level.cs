using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float cameraSize = 50f;
    private const float pipeWidth = 7.8f;
    private const float pipeHeadHeight = 3.75f;
    private const float pipeSpeed = 3f;
    private const float pipeDestroyPos = -120f;

    private List<Pipe> pipeList;

    private void Awake()
    {
        pipeList = new List<Pipe>();
    }
    private void Start()
    {
        createGap(40f, 20f, 10f);
    }
    
    private void createGap(float y, float size, float pos)
    {
        createPipe(y - size * .5f, pos, true);
        createPipe(cameraSize * 2f - y - size * .5f, pos, false);
    }

    private void Update()
    {
        handleMovement();
    }

    private void handleMovement()
    {
        for (int i = 0; i < pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            pipe.Move();
            if (pipe.getPos() < pipeDestroyPos) { 
                //destroy pipe and remove, minus an index so that it doesnt skip an index after being removed
                pipe.destroySelf();
                pipeList.Remove(pipe);
                i--;
            }
        }
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
        Pipe pipe = new Pipe(pipeHead, pipeBody);
        pipeList.Add(pipe);
    }

    private class Pipe
    {
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
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

        public void destroySelf()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }

}


