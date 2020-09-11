namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }

        }

        public bool IsRootDeleted { get; private set; }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            this.Dfs(this, result);

            return result;
        }


        public void AddChild(T parentKey, Tree<T> child)
        {
            var searchedNode = this.FindBfs(parentKey);

            ChekEmptyNode(searchedNode);

            searchedNode._children.Add(child);
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);
            ChekEmptyNode(searchedNode);

            foreach (var child in searchedNode.Children)
            {
                child.Parent = null;
            }

            searchedNode._children.Clear();

            var searchedParent = searchedNode.Parent;

            if (searchedParent == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                searchedParent._children.Remove(searchedNode);
            }

            searchedNode.Value = default(T);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);
            this.ChekEmptyNode(firstNode);
            this.ChekEmptyNode(secondNode);

            var firstParent = firstNode.Parent;
            var secondParent = secondNode.Parent;

            if (firstParent == null)
            {
                this.SwapRoot(secondNode);
                return;
            }

            if (secondParent == null)
            {
                this.SwapRoot(firstNode);
                return;
            }

            firstNode.Parent = secondParent;
            secondNode.Parent = firstParent;

            int indexOfFirst = firstParent._children.IndexOf(firstNode);
            int indexOfSecond = secondParent._children.IndexOf(secondNode);

            firstParent._children[indexOfFirst] = secondNode;
            secondParent._children[indexOfSecond] = firstNode;
        }

        private void SwapRoot(Tree<T> secondNode)
        {
            this.Value = secondNode.Value;
            this._children.Clear();
            foreach (var child in secondNode.Children)
            {
                this._children.Add(child);
            }
        }

        private void Dfs(Tree<T> subtree, List<T> result)
        {
            foreach (var child in subtree.Children)
            {
                this.Dfs(child, result);
            }

            result.Add(subtree.Value);
        }

        private Tree<T> FindBfs(T parentKey)
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var subtree = queue.Dequeue();

                if (subtree.Value.Equals(parentKey))
                {
                    return subtree;
                }

                result.Add(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        private void ChekEmptyNode(Tree<T> searchedNode)
        {
            if (searchedNode == null)
            {
                throw new ArgumentNullException("Searched node not found!");
            }
        }
    }
}