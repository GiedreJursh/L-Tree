// Unity Includes:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Tree : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject root;
    public int counter;
    public char axiom = 'A';
    public float maxLength = 2.0f;
    public float maxGirth = 1.0f;
    public float maxSpeed = 5.0f;
    public float branch_size = 0.0f;
    public int curentItem = 0;

    public List<GameObject> turtlrArray = new List<GameObject>();
    public List<char> conditions = new List<char>();
    public List<float> leafY = new List<float>();

    void Start()
    {
        counter = 0;
        conditions.Add(axiom);

        // rewrite 3 times to make big plant:
        for (int i = 0; i < 3; i++)
        {
            Rewrite();
        }

        // Translate apthabet into 3D information:
        Turtle_Translate();

        // To create a growing/spredding effect the geometry first must be scaled into nothingness:
        for (int i = 0; i < turtlrArray.Count; i++)
        {
            GameObject branch_c = turtlrArray[i].transform.Find("Cylinder").gameObject;
            branch_c.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    // Warrning: tree might look waky if speed in witch it rows is too fast!
    void Update()
    {
        // Scales branch i local axes every frame untill maximum length is reached:
        if (branch_size < maxLength)
        {
            // Speed in wich it branch is growing:
            branch_size += Time.deltaTime * maxSpeed;

            // If curent item does not exide total count of branches scale curent branch:
            if (curentItem < turtlrArray.Count)
            {
                // Find the right branch:
                GameObject branch_c = turtlrArray[curentItem].transform.Find("Cylinder").gameObject;
                // Scale the branch in y-axes:
                branch_c.transform.localScale = new Vector3(branch_c.transform.localScale.x, branch_size, branch_c.transform.localScale.z);
                // Adjust position so it scales from the bottom not for the middle:
                branch_c.transform.localPosition = new Vector3(branch_c.transform.localPosition.x, (0.0f + branch_c.transform.localScale.y), branch_c.transform.localPosition.z);
                // Sacle branch in x- and z-axes untill the lenght is reached:
                if (branch_size < maxGirth)
                {
                    branch_c.transform.localScale = new Vector3(branch_size, branch_c.transform.localScale.y, branch_size);
                }
            }
        }
        // If curent size is reached:
        else
        {
            // And if there are more branches Reset branch size and move on to another branch:
            if (curentItem < turtlrArray.Count)
            {
                branch_size = 0.0f;
                curentItem++;
            }
        }
    }

    // Rewrites generations using aphabet's rules:
    void Rewrite()
    {
        List<char> temp = new List<char>();
        for (int i = 0; i < conditions.Count; i++)
        {

            //A = [++BB[--C] [++C] [__C] [^^C] A]<<<<<+BBB[--C][++C][__C][^^C]A
            if (conditions[i] == 'A')
            {
                temp.Add('[');
                temp.Add('+');
                temp.Add('+');
                temp.Add('B');
                temp.Add('B');
                temp.Add('[');
                temp.Add('-');
                temp.Add('-');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('+');
                temp.Add('+');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('_');
                temp.Add('_');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('^');
                temp.Add('^');
                temp.Add('C');
                temp.Add(']');
                temp.Add('A');
                temp.Add(']');
                temp.Add('<');
                temp.Add('<');
                temp.Add('<');
                temp.Add('<');
                temp.Add('<');
                temp.Add('+');
                temp.Add('B');
                temp.Add('B');
                temp.Add('B');
                temp.Add('[');
                temp.Add('-');
                temp.Add('-');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('+');
                temp.Add('+');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('_');
                temp.Add('_');
                temp.Add('C');
                temp.Add(']');
                temp.Add('[');
                temp.Add('^');
                temp.Add('^');
                temp.Add('C');
                temp.Add(']');
                temp.Add('A');
            }

            // B = >>B
            else if (conditions[i] == 'B')
            {
                temp.Add('>');
                temp.Add('>');
                temp.Add('B');
            }

            // C = C
            else if (conditions[i] == 'C')
            {
                temp.Add('C');
            }

            else if (conditions[i] == '+')
            {
                temp.Add('+');
            }
            else if (conditions[i] == '-')
            {
                temp.Add('-');
            }
            else if (conditions[i] == '<')
            {
                temp.Add('<');
            }
            else if (conditions[i] == '>')
            {
                temp.Add('>');
            }
            else if (conditions[i] == '^')
            {
                temp.Add('^');
            }
            else if (conditions[i] == '_')
            {
                temp.Add('_');
            }
            else if (conditions[i] == '[')
            {
                temp.Add('[');
            }
            else if (conditions[i] == ']')
            {
                temp.Add(']');
            }
        }

        conditions.Clear();

        for (int i = 0; i < temp.Count; i++)
        {
            conditions.Add(temp[i]);
        }
    }

    //Generates random angle:
    float RandomAngle()
    {
        return Random.Range(10.0f, 20.0f);
    }

    //Translates alphabet into 3D using turtle graphics principal:
    void Turtle_Translate()
    {
        Vector3 leafPosition = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 branchRotation = new Vector3(0.0f, 0.0f, 0.0f);
        List<Vector3> turtleList = new List<Vector3>();
        List<Vector3> turtleRotation = new List<Vector3>();

        for (int i = 0; i < conditions.Count; i++)
        {
            if (conditions[i] == 'A' || conditions[i] == 'B' || conditions[i] == 'C')
            {
                // Create branch:
                GameObject branch = Instantiate(root);
                GameObject branch_c = branch.transform.Find("Cylinder").gameObject;
                GameObject leaf = branch_c.transform.Find("l_branch").gameObject;

                float oldLeafLoc = leaf.transform.position.y;
                branch_c.transform.localScale = new Vector3(branch_c.transform.localScale.x, (branch_c.transform.localScale.y * 1.0f), branch_c.transform.localScale.z);
                float newLeafLoc = leaf.transform.position.y;
                float dif = oldLeafLoc - newLeafLoc;

                branch_c.transform.position = new Vector3(leaf.transform.position.x, (branch_c.transform.position.y - dif), leaf.transform.position.z);
                branch.transform.Rotate(branchRotation, Space.Self);

                branch.transform.position = leafPosition;
                leafPosition = leaf.transform.position;

                turtlrArray.Add(branch);
                leafY.Add(leafPosition.y);
            }
            else if (conditions[i] == '+') //Roll
            {
                // Rotate + degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(0.0f, 0.0f, rAngle);
            }
            else if (conditions[i] == '-') //-Roll
            {
                // Rotate - degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(0.0f, 0.0f, ((-1.0f) * rAngle));
            }
            else if (conditions[i] == '<') //Yaw
            {
                // Rotate + degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(0.0f, rAngle, 0.0f);
            }
            else if (conditions[i] == '>') //-Yaw
            {
                // Rotate - degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(0.0f, ((-1.0f) * rAngle), 0.0f);
            }
            else if (conditions[i] == '^') //Pich
            {
                // Rotate + degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(rAngle, 0.0f, 0.0f);
            }
            else if (conditions[i] == '_') //-Pich
            {
                // Rotate - degrees:
                float rAngle = RandomAngle();
                branchRotation += new Vector3(((-1.0f) * rAngle), 0.0f, 0.0f);
            }
            else if (conditions[i] == '[')
            {
                // Push:
                turtleList.Add(leafPosition);

                turtleRotation.Add(branchRotation);

            }
            else if (conditions[i] == ']')
            {
                // Pop:
                leafPosition = turtleList[turtleList.Count - 1];
                branchRotation = turtleRotation[turtleRotation.Count - 1];

                turtleList.Remove(leafPosition);
                turtleRotation.Remove(branchRotation);
            }
        }
    }
}

// This code was inspired by The Coding Train's video that was also used as a reference in some places.
// Here is the link to it https://www.youtube.com/watch?v=E1B4UoSQMFw
// To make it into 3D this page was used as reference: https://jobtalle.com/lindenmayer_systems.html
