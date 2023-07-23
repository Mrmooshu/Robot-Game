using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourSystem
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class BehaviourNode
    {
        protected NodeState state;

        public BehaviourNode parent;
        protected List<BehaviourNode> children = new List<BehaviourNode>();

        private Dictionary<string, object> dataContext = new Dictionary<string, object>();

        public BehaviourNode()
        {
            parent = null;
        }

        public BehaviourNode(List<BehaviourNode> children)
        {
            foreach (BehaviourNode child in children)
            {
                Attach(child);
            }
        }

        private void Attach(BehaviourNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            BehaviourNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (dataContext.ContainsKey(key))
            {
                dataContext.Remove(key);
                return true;
            }

            BehaviourNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.parent;
            }
            return false;
        }

        public BehaviourNode GetRoot()
        {
            if (parent != null)
            {
                return parent.GetRoot();
            }
            return this;
        }
    }
}


