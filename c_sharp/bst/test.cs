using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;

namespace Bst {

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