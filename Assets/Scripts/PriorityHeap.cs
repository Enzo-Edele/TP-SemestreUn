using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node<T>
{
    public T content;
    public int priority;
    public int index;
}

public class PriorityHeap<T>
{
    List<Node<T>> heap;

    public PriorityHeap()
    {
        heap = new List<Node<T>>();
    }

    public Node<T> Insert(T content, int priority)
    {
        Node<T> newElem = new Node<T>();
        newElem.content = content;
        newElem.priority = priority;
        newElem.index = heap.Count;
        heap.Add(newElem);
        GoUp(heap.Count - 1);
        return newElem;
    }

    public void ChangePriority(Node<T> node, int newPrio)
    {
        if (node.index >= heap.Count) throw new IndexOutOfRangeException();
        if (heap[node.index] != node) throw new InvalidOperationException();
        int oldPrio = node.priority;
        node.priority = newPrio;
        if (oldPrio > newPrio)
            GoUp(node.index);
        else if (newPrio > oldPrio)
            GoDown(node.index);
    }

    public Node<T> Search(T content)
    {
        foreach (Node<T> n in heap)
            if (n.content.Equals(content)) return n;
        return null;
    }

    public Node<T> GetMinNode()
    {
        return heap[0];
    }

    public Node<T> PopMin()
    {
        Node<T> res = GetMinNode();
        Swap(0, heap.Count - 1);
        heap.RemoveAt(heap.Count - 1);
        GoDown(0);
        return res;
    }

    void GoDown(int node)
    {
        while (HasLeftChild(node))
        {
            int minNode = LeftChild(node);
            if (HasRightChild(node) &&
                heap[RightChild(node)].priority < heap[LeftChild(node)].priority)
                minNode = RightChild(node);

            if (heap[minNode].priority < heap[node].priority)
            {
                Swap(minNode, node);
                node = minNode;
            }
            else return;
        }
    }

    void GoUp(int node)
    {
        while (!IsRoot(node))
        {
            if (heap[Parent(node)].priority > heap[node].priority)
            {
                Swap(node, Parent(node));
                node = Parent(node);
            }
            else return;
        }
    }

    void Swap(int node1, int node2)
    {
        Node<T> tmp = heap[node1];
        heap[node1] = heap[node2];
        heap[node2] = tmp;

        heap[node1].index = node1;
        heap[node2].index = node2;
    }

    int LeftChild(int node) { return (node * 2) + 1; }
    int RightChild(int node) { return (node * 2) + 2; }
    int Parent(int node) { return (node - 1) / 2; }

    bool HasLeftChild(int node) { return LeftChild(node) < heap.Count; }
    bool HasRightChild(int node) { return RightChild(node) < heap.Count; }
    bool IsRoot(int node) { return node == 0; }

    public bool IsEmpty() { return heap.Count == 0; }
}
