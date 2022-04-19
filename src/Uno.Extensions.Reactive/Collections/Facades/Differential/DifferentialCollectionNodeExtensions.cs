﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Uno.Extensions.Collections.Facades.Differential;

/// <summary>
/// Extensions over <see cref="IDifferentialCollectionNode"/>.
/// </summary>
internal static class DifferentialCollectionNodeExtensions
{
	/// <summary>
	/// Returns an enumerator that iterates through the differential collection.
	/// </summary>
	/// <param name="head">The node from which enumeration should start</param>
	/// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the differential collection.</returns>
	public static IEnumerator GetEnumerator(this IDifferentialCollectionNode head)
		=> new Enumerator(head);

	/// <summary>
	/// Returns an enumerator that iterates through the differential collection.
	/// </summary>
	/// <param name="head">The node from which enumeration should start</param>
	/// <returns>An <see cref="IEnumerator{T}"/> object that can be used to iterate through the differential collection.</returns>
	public static IEnumerator<T> GetEnumerator<T>(this IDifferentialCollectionNode head)
		=> new Enumerator<T>(head);

	/// <summary>
	/// Creates a wrapper which allow to use the given node as an <see cref="IList"/>.
	/// </summary>
	/// <param name="head">The nodes to adapt.</param>
	/// <returns>A readonly list which wraps the provided node.</returns>
	public static IList AsList(this IDifferentialCollectionNode head)
		=> new DifferentialReadOnlyList(head);

	/// <summary>
	/// Creates a wrapper which allow to use the given node as an <see cref="IList"/>.
	/// </summary>
	/// <param name="head">The nodes to adapt.</param>
	/// <returns>A readonly list which wraps the provided node.</returns>
	public static IList<T> AsList<T>(this IDifferentialCollectionNode head)
		=> new DifferentialReadOnlyList<T>(head);

	/// <summary>
	/// Creates a wrapper which allow to use the given node as an <see cref="IList"/>.
	/// </summary>
	/// <param name="head">The nodes to adapt.</param>
	/// <returns>A readonly list which wraps the provided node.</returns>
	public static IReadOnlyList<T> AsReadOnlyList<T>(this IDifferentialCollectionNode head)
		=> new DifferentialReadOnlyList<T>(head);

	///// <summary>
	///// Creates a new <seealso cref="IDifferentialCollectionNode"/> over the given node which reflects the provided change
	///// </summary>
	///// <param name="node">The current head of the collection.</param>
	///// <param name="args">The change to apply over the current <paramref name="node"/>.</param>
	///// <returns>A collection head node, which correspond to the current modified by the provided args.</returns>
	//public static IDifferentialCollectionNode Add(this IDifferentialCollectionNode node, RichNotifyCollectionChangedEventArgs args)
	//{
	//	switch (args.Action)
	//	{
	//		case NotifyCollectionChangedAction.Add:
	//			return new Add(node, args);

	//		case NotifyCollectionChangedAction.Move:
	//			return new Move(node, args);

	//		case NotifyCollectionChangedAction.Remove:
	//			return new Remove(node, args);

	//		case NotifyCollectionChangedAction.Replace:
	//			return new Replace(node, args);

	//		case NotifyCollectionChangedAction.Reset:
	//			return new Reset(args.ResetNewItems);

	//		default:
	//			throw new ArgumentOutOfRangeException(nameof(args), $"Unkown action '{args.Action}'.");
	//	}
	//}
}
