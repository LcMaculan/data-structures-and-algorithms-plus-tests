using System;
using System.Collections.Generic;

namespace BreadthFirstSearch {
  public class Queue {
    public List<Node> queue;

    public Queue() {
      this.queue = new List<Node>();
    }

    public bool isEmpty() {
      return this.queue.Count == 0;
    }

    public void enqueue(Node node) {
      this.queue.Add(node);
    }

    public Node dequeue() {
      var result = this.queue[0];
      this.queue.RemoveAt(0);
      return result;
    }

    public void empty() {
      this.queue.Clear();
    }

  }

  public class Node {
    public List<Node> adj;
    public String color;
    public int? d;
    public Node p;
    public int? value;

    public Node() {
      this.adj = new List<Node>();
      this.color = null;
      this.d = null;
      this.p = null;
      this.value = null;
    }

    public Node(int value) : this() {
      this.value = value;
    }
  }

  public class Graph {
    public List<Node> vertices;
    private Queue queue;

    // edges
    public Graph() {
      this.vertices = new List<Node>();
      this.queue = new Queue();
    }

    public Graph(Node node) : this() {
      Graph.initalizeVertices(this, node);
    }

    public static void initalizeVertices(Graph graph, Node node) {
      graph.vertices.Add(node);
      foreach(Node n in node.adj) {
        Graph.initalizeVertices(graph, n);
      }
    }

    public void bfs(Node node) {
      foreach (Node n in this.vertices) {
        n.color = "white";
        n.d = int.MaxValue;
        n.p = null;
      }
      node.color = "gray";
      node.d = 0;
      this.queue.empty();
      this.queue.enqueue(node);
      while (!this.queue.isEmpty()) {
        var n = this.queue.dequeue();
        foreach(Node m in n.adj) {
          if (m.color == "white") {
            m.p = n;
            m.color = "gray";
            m.d = n.d + 1;
            this.queue.enqueue(m);
          }
        }
        n.color = "black";
      }
    }

  }

}