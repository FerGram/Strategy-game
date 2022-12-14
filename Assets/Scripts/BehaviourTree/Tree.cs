using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    //<summary>
    //Clase �rbol para implementar el �rbol de comportamientos
    //<summary/>
    public abstract class Tree : MonoBehaviour
    {
        private TreeNode root = null;

        protected void Start()
        {
            root = SetUpTree();
        }
        private void Update()
        {
            if (root != null)
                root.Evaluate();
        }

        protected abstract TreeNode SetUpTree();
    }
}

