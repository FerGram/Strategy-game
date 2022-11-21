using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // <sumamary>
    // Clase nodo básica para implementar el árbol de comportamientos.
    // </summary>

    public enum TreeNodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    public class TreeNode
    {
        protected TreeNodeState state;
        public TreeNode parent;

        //Todos los hijos de cada nodo estarán en esta lista
        protected List<TreeNode> children = new List<TreeNode>();

        //Los datos de cada nodo se almacenan en este diccionario
        private Dictionary<string, object> data = new Dictionary<string, object>();

        //Constructores
        public TreeNode()
        {
            parent = null;
        }

        public TreeNode(List<TreeNode> children)
        {
            foreach (TreeNode child in children)
                Attach(child);
        }

        //Método para asociar un hijo a un padre
        private void Attach(TreeNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual TreeNodeState Evaluate() => TreeNodeState.FAILURE;

        //<summary>
        //Conjunto de métodos para hacer operaciones con los datos de los nodos usando el diccionario data.
        //SetData: Añadir un dato value al nodo con la clave key
        //GetData: Obtener el dato (object) asociado a la clave key
        //ClearData: Eliminar todo dato en el nodo con clave key
        //<summary/>

        public void SetData(string key, object value)
        {
            data[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (data.TryGetValue(key, out value))
                return value;

            TreeNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }
        public bool ClearData(string key)
        {
            if (data.ContainsKey(key))
            {
                data.Remove(key);
                return true;
            }
                
            TreeNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}


    