using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace Tree {

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
    public Node r;
    public Node l;
    public Node p;
    public int? value;

    private Node() {
      this.r = null;
      this.l = null;
      this.p = null;
      this.value = null;
    }

    public Node(int value) : this() {
      this.value = value;
    }
  }

  public class Tree {
    public Node root;

    public Tree() {
      this.root = null;
    }

    public void add(int value) {
      Node y = null;
      Node x = this.root;
      while (x != null) {
        y = x;
        if (value > x.value) { x = x.r; }
        else { x = x.l; }
      }

      if (y == null) {
        this.root = new Node(value);
        return;
      }

      var t = new Node(value);
      if (value > y.value) { y.r = t; }
      else { y.l = t; }
      t.p = y;
    }

    public static Node min(Node node) {
      while (node.l != null) {
        node = node.l;
      }
      return node;
    }
    public Node min() {
      return Tree.min(this.root);
    }

    public static Node max(Node node) {
      while (node.r != null) {
        node = node.r;
      }
      return node;
    }
    public Node max() {
      return Tree.max(this.root);
    }

    public void leftRotate(Node node) {
      var y = node.r;
      var x = y.l;

      if (node.p == null) {
        y.p = null;
        this.root = y;
      }
      else {
        y.p = node.p;
        if (node.p.l == node) { node.p.l = y; }
        else { node.p.r = y; }
      }

      node.p = y;
      if (x != null) { x.p = node; }

      node.r = x;
      y.l = node;
    }

    public void rightRotate(Node node) {
      var y = node.l;
      var x = y.r;

      if (node.p == null) {
        y.p = null;
        this.root = y;
      }
      else {
        y.p = node.p;
        if (node.p.r == node) { node.p.r = y; }
        else { node.p.l = y; }
      }

      node.p = y;
      if (x != null) { x.p = node; }

      node.l = x;
      y.r = node;
    }

    public static Node successor(Node node) {
      if (node.r != null) {
        return Tree.min(node.r);
      }

      var p = node.p;
      while (p != null && p.r == node) {
        node = p;
        p = node.p;
      }
      return node;
    }

    public static Node predecessor(Node node) {
      if (node.l != null) {
        return Tree.max(node.l);
      }

      var p = node.p;
      while (p != null && p.l == node) {
        node = p;
        p = node.p;
      }
      return node;
    }

    public void deleteNode(Node node) {
      Node y = null;
      if (node.l == null || node.r == null) {
        y = node;
      }
      else {
        y = Tree.successor(node);
      }

      Node x = null;
      if (y.l == null) { x = y.r; }
      else { x = y.l; }

      if (y.p == null) { this.root = x; }
      else {
        if (y.p.l == y) { y.p.l = x; }
        else { y.p.r = x; }
      }

      if (x != null) {
        x.p = y.p;
      }

      if (y.value != node.value) {
        node.value = y.value;
      }
    }

    public List<Node> GetNodes() {
      var queue = new Queue();
      var result_nodes = new List<Node>();
      queue.enqueue(this.root);
      
      while (!queue.isEmpty()) {
        var x = queue.dequeue();
        result_nodes.Add(x);
        if (x.l != null) { queue.enqueue(x.l); }
        if (x.r != null) { queue.enqueue(x.r); }
      }
      return result_nodes;
    }

    private static bool isCorrect(Node node, int? max, int? min) {
      var queue = new Queue();
      var result_isCorrect = true;

      if (node.l != null) { queue.enqueue(node.l); }
      if (node.r != null) { queue.enqueue(node.r); }

      while (result_isCorrect && !queue.isEmpty()) {
        var x = queue.dequeue();

        if (max != null && x.value >= max) { result_isCorrect = false; }
        if (min != null && x.value < min) { result_isCorrect = false; }

        if (result_isCorrect && x.l != null) { queue.enqueue(x.l); }
        if (result_isCorrect && x.r != null) { queue.enqueue(x.r); }
      }

      return result_isCorrect;
    }

    public bool isCorrect() {
      var queue = new Queue();
      var result_isCorrect = true;

      if (this.root == null) { return result_isCorrect; }

      queue.enqueue(this.root);
      while (result_isCorrect && !queue.isEmpty()) {
        var x = queue.dequeue();
        if (result_isCorrect && x.r != null) {
          result_isCorrect = Tree.isCorrect(x.r, null, x.value);
          queue.enqueue(x.r);
        }
        if (result_isCorrect && x.l != null) {
          result_isCorrect = Tree.isCorrect(x.l, x.value, null);
          queue.enqueue(x.l);
        }
      }

      return result_isCorrect;
    }

    private void inOrder(Node node) {
      if (node == null) { return; }

      this.inOrder(node.l);
      Console.WriteLine(node.value);
      this.inOrder(node.r);
    }

    public void inOrder() {
      this.inOrder(this.root);
    }
  }

  [TestFixture]
  public class TreeSpec {
    [Test]
    public void EmptyTree() {
      var tree = new Tree();
      Assert.AreEqual(tree.root, null);
    }

    [Test]
    public void RootTree() {
      var tree = new Tree();

      tree.add(1);
      Assert.AreEqual(tree.root.value, 1);
    }

    [Test]
    public void AddTreeOne() {
      var tree = new Tree();

      tree.add(2);
      tree.add(3);
      tree.add(1);
      Assert.AreEqual(tree.root.value, 2);
      Assert.AreEqual(tree.root.r.value, 3);
      Assert.AreEqual(tree.root.l.value, 1);
      Assert.AreEqual(tree.root.r.p, tree.root);
      Assert.AreEqual(tree.root.l.p, tree.root);
    }

    [Test]
    public void AddTreeTwo() {
      var tree = new Tree();

      tree.add(2);
      tree.add(3);
      tree.add(4);
      Assert.AreEqual(tree.root.value, 2);
      Assert.AreEqual(tree.root.r.value, 3);
      Assert.AreEqual(tree.root.r.r.value, 4);
      Assert.AreEqual(tree.root.r.r.p, tree.root.r);
      Assert.AreEqual(tree.root.r.p, tree.root);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void AddTreeThree() {
      var tree = new Tree();

      tree.add(2);
      tree.add(3);
      tree.add(4);
      Assert.AreEqual(tree.root.value, 2);
      Assert.AreEqual(tree.root.r.value, 3);
      Assert.AreEqual(tree.root.r.r.value, 4);
      Assert.AreEqual(tree.root.r.r.p, tree.root.r);
      Assert.AreEqual(tree.root.r.p, tree.root);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void MinTree() {
      var tree = new Tree();

      tree.add(4);
      tree.add(3);
      tree.add(2);
      Assert.AreEqual(tree.min().value, 2);
    }

    [Test]
    public void MaxTree() {
      var tree = new Tree();

      tree.add(2);
      tree.add(3);
      tree.add(4);
      Assert.AreEqual(tree.max().value, 4);
    }

    [Test]
    public void LeftRotateTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(12);
      Assert.AreEqual(tree.root.value, 10);
      Assert.AreEqual(tree.root.l.value, 5);
      Assert.AreEqual(tree.root.r.value, 15);
      Assert.AreEqual(tree.root.r.l.value, 12);

      tree.leftRotate(tree.root);
      Assert.AreEqual(tree.root.value, 15);
      //
      Assert.AreEqual(tree.root.l.value, 10);
      Assert.AreEqual(tree.root.l.p, tree.root);
      //
      Assert.AreEqual(tree.root.l.l.value, 5);
      Assert.AreEqual(tree.root.l.l.p, tree.root.l);
      //
      Assert.AreEqual(tree.root.l.r.value, 12);
      Assert.AreEqual(tree.root.l.r.p, tree.root.l);
      //
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void RightRotateTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      Assert.AreEqual(tree.root.value, 10);
      Assert.AreEqual(tree.root.l.value, 5);
      Assert.AreEqual(tree.root.r.value, 15);
      Assert.AreEqual(tree.root.l.r.value, 8);

      tree.rightRotate(tree.root);
      Assert.AreEqual(tree.root.value, 5);
      //
      Assert.AreEqual(tree.root.r.value, 10);
      Assert.AreEqual(tree.root.r.p, tree.root);
      //
      Assert.AreEqual(tree.root.r.l.value, 8);
      Assert.AreEqual(tree.root.r.l.p, tree.root.r);
      //
      Assert.AreEqual(tree.root.r.r.value, 15);
      Assert.AreEqual(tree.root.r.r.p, tree.root.r);
      //
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void DeleteLeftRightNullTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);

      var nodeToDelete = tree.root.l.r;
      var nodeToDeleteValue = nodeToDelete.value;
      tree.deleteNode(nodeToDelete);
      Assert.AreEqual(tree.GetNodes().Where(x => nodeToDeleteValue == x.value).Count(), 0);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void DeleteUsingSuccessorTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      tree.add(6);
      tree.add(9);

      var nodeToDelete = tree.root.l.r;
      var nodeToDeleteValue = nodeToDelete.value;
      tree.deleteNode(nodeToDelete);
      Assert.AreEqual(tree.GetNodes().Where(x => nodeToDeleteValue == x.value).Count(), 0);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void DeleteRightNullTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      tree.add(6);

      var nodeToDelete = tree.root.l.r;
      var nodeToDeleteValue = nodeToDelete.value;
      tree.deleteNode(nodeToDelete);
      Assert.AreEqual(tree.GetNodes().Where(x => nodeToDeleteValue == x.value).Count(), 0);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void DeleteLeftNull() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      tree.add(9);

      var nodeToDelete = tree.root.l.r;
      var nodeToDeleteValue = nodeToDelete.value;
      tree.deleteNode(nodeToDelete);
      Assert.AreEqual(tree.GetNodes().Where(x => nodeToDeleteValue == x.value).Count(), 0);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void DeleteRootTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      tree.add(9);

      var nodeToDelete = tree.root;
      var nodeToDeleteValue = nodeToDelete.value;
      tree.deleteNode(nodeToDelete);
      Assert.AreEqual(tree.GetNodes().Where(x => nodeToDeleteValue == x.value).Count(), 0);
      Assert.IsTrue(tree.isCorrect());
    }

    [Test]
    public void CheckTree() {
      var tree = new Tree();

      tree.add(10);
      tree.add(5);
      tree.add(15);
      tree.add(8);
      tree.add(9);

      Assert.IsTrue(tree.isCorrect());

      tree.root.l.r.value = 100;
      Assert.IsFalse(tree.isCorrect());
    }

  }

}