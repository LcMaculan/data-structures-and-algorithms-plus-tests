using System;
using System.Collections.Generic;

namespace LinkedList {

  public class Node {
    public int value;
    public Node next = null;

    public Node(int value) {
      this.value = value;
    }
  }

  public class LinkedList {
    public Node root = null;

    public void add(int value) {
      if (this.root == null) {
        this.root = new Node(value);
        return;
      }

      Node currentNode = this.root;
      while (currentNode.next != null) {
        currentNode = currentNode.next;
      }
      currentNode.next = new Node(value);
    }

    public int length() {
      if (this.root == null) {
        return 0;
      }

      int length = 1;
      Node currentNode = this.root;
      while (currentNode.next != null) {
        length += 1;
        currentNode = currentNode.next;
      }
      return length;
    }

    public int? valueAt(int i) {
      Node currentNode = this.root;
      i -= 1;
      while (currentNode != null && i >= 0) {
        currentNode = currentNode.next;
        i -= 1;
      }
      if (currentNode != null && i < 0) {
        return currentNode.value;
      }
      return null;
    }

    public int? indexOf(int value) {
      var currentNode = this.root;
      var idx = 0;
      while (currentNode != null && currentNode.value != value) {
        currentNode = currentNode.next;
        idx += 1;
      }
      if (currentNode == null) { return null; }
      return idx;
    }

    public void remove(int value) {
      var previousNode = (Node)null;
      var currentNode = this.root;
      while (currentNode != null && currentNode.value != value) {
        previousNode = currentNode; 
        currentNode = currentNode.next;
      }
      if (currentNode == null) { return; }

      if (previousNode == null) {
        this.root = currentNode.next;
      }
      previousNode.next = previousNode.next.next;
    }

    private static Node reverse(Node node) {
      if (node == null || node.next == null) {
        return node;
      }

      Node reversedNode = LinkedList.reverse(node.next);

      node.next.next = node;
      node.next = null;

      return reversedNode;
    }

    public void reverseList() {
      this.root = LinkedList.reverse(this.root);
    }

    public override string ToString(){
      var result = "";
      Node currentNode = this.root;
      while (currentNode != null) {
        result += currentNode.value;
        result += " -> ";
        currentNode = currentNode.next;
      }
      result += "NULL";

      return result;
    }

  }

}