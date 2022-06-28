using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace LinkedList {

  [TestFixture]
  public class LinkedListSpec {

    [Test]
    public void EmptyLinkedList() {
      LinkedList linkedList = new LinkedList();
      Assert.AreEqual(linkedList.root, null);
    }

    [Test]
    public void AddElement() {
      LinkedList linkedList = new LinkedList();

      linkedList.add(10);
      Assert.AreEqual(linkedList.root.value, 10);
      Assert.AreEqual(linkedList.length(), 1);
    }

    [Test]
    public void AddManyElements() {
      LinkedList linkedList = new LinkedList();

      linkedList.add(10);
      linkedList.add(11);
      Assert.AreEqual(linkedList.length(), 2);
    }

    [Test]
    public void RetriveElement() {
      LinkedList linkedList = new LinkedList();

      Assert.AreEqual(linkedList.valueAt(0), null);
      Assert.AreEqual(linkedList.valueAt(10), null);

      linkedList.add(10);
      linkedList.add(11);
      linkedList.add(12);
      linkedList.add(13);
      Assert.AreEqual(linkedList.valueAt(0), 10);
      Assert.AreEqual(linkedList.valueAt(1), 11);
      Assert.AreEqual(linkedList.valueAt(2), 12);
      Assert.AreEqual(linkedList.valueAt(3), 13);
      Assert.AreEqual(linkedList.length(), 4);
    }

    [Test]
    public void RetriveIndex() {
      LinkedList linkedList = new LinkedList();

      Assert.AreEqual(linkedList.indexOf(0), null);
      Assert.AreEqual(linkedList.indexOf(10), null);

      linkedList.add(10);
      linkedList.add(11);
      linkedList.add(12);
      linkedList.add(13);
      Assert.AreEqual(linkedList.indexOf(10), 0);
      Assert.AreEqual(linkedList.indexOf(11), 1);
      Assert.AreEqual(linkedList.indexOf(12), 2);
      Assert.AreEqual(linkedList.indexOf(13), 3);
    }

    [Test]
    public void RemoveElement() {
      LinkedList linkedList = new LinkedList();

      linkedList.remove(1);
      linkedList.add(10);
      linkedList.add(11);
      linkedList.add(12);
      linkedList.add(13);
      linkedList.remove(11);
      Assert.AreEqual(linkedList.indexOf(11), null);
      Assert.AreEqual(linkedList.length(), 3);
    }

    [Test]
    public void ReverseList() {
      LinkedList linkedList = new LinkedList();

      linkedList.add(10);
      linkedList.add(11);
      linkedList.add(12);
      linkedList.add(13);
      linkedList.reverseList();
      Assert.AreEqual(linkedList.valueAt(0), 13);
      Assert.AreEqual(linkedList.valueAt(1), 12);
      Assert.AreEqual(linkedList.valueAt(2), 11);
      Assert.AreEqual(linkedList.valueAt(3), 10);
    }

  }

}