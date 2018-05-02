using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A source of values for <see cref="ICompositeAggregation"/>
	/// </summary>
	public interface ICompositeAggregationSource
	{
		/// <summary>
		/// The name of the source
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// The field from which to extract value
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// Defines the direction of sorting for each
		/// value source. Defaults to <see cref="SortOrder.Ascending"/>
		/// </summary>
		[JsonProperty("order")]
		SortOrder? Order { get; set; }
	}

	/// <inheritdoc />
	public abstract class CompositeAggregationSourceBase : ICompositeAggregationSource
	{
		/// <inheritdoc />
		string ICompositeAggregationSource.Name { get; set; }

		protected CompositeAggregationSourceBase(string name) =>
			((ICompositeAggregationSource)this).Name = name;

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public SortOrder? Order { get; set; }
	}

	/// <inheritdoc cref="ICompositeAggregationSource"/>
	public class CompositeAggregationSourcesDescriptor<T> :
		DescriptorPromiseBase<CompositeAggregationSourcesDescriptor<T>, IList<ICompositeAggregationSource>>
		where T : class
	{
		public CompositeAggregationSourcesDescriptor() : base(new List<ICompositeAggregationSource>()) {}

		public CompositeAggregationSourcesDescriptor<T> Terms(string name, Func<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new TermsCompositeAggregationSourceDescriptor<T>(name))));

		public CompositeAggregationSourcesDescriptor<T> Histogram(string name, Func<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new HistogramCompositeAggregationSourceDescriptor<T>(name))));

		public CompositeAggregationSourcesDescriptor<T> DateHistogram(string name, Func<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource> selector) =>
			Assign(a => a.Add(selector?.Invoke(new DateHistogramCompositeAggregationSourceDescriptor<T>(name))));
	}

	public abstract class CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>
		: DescriptorBase<TDescriptor, TInterface>, ICompositeAggregationSource
		where TDescriptor : CompositeAggregationSourceDescriptorBase<TDescriptor, TInterface, T>, TInterface
		where TInterface : class, ICompositeAggregationSource
	{
		protected CompositeAggregationSourceDescriptorBase(string name) => Self.Name = name;

		string ICompositeAggregationSource.Name { get; set; }
		Field ICompositeAggregationSource.Field { get; set; }
		SortOrder? ICompositeAggregationSource.Order { get; set; }

		public TDescriptor Field(Field field) => Assign(a => a.Field = field);

		public TDescriptor Field(Expression<Func<T,object>> objectPath) => Assign(a => a.Field = objectPath);

		public TDescriptor Order(SortOrder? order) => Assign(a => a.Order = order);
	}
}
