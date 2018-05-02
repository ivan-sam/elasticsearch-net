using Newtonsoft.Json;

namespace Nest
{
	public interface IHistogramCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("interval")]
		int Interval { get; set; }
	}

	public class HistogramCompositeAggregationSource : CompositeAggregationSourceBase, IHistogramCompositeAggregationSource
	{
		public HistogramCompositeAggregationSource(string name) : base(name) {}

		/// <inheritdoc />
		public int Interval { get; set; }
	}

	public class HistogramCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<HistogramCompositeAggregationSourceDescriptor<T>, IHistogramCompositeAggregationSource, T>, IHistogramCompositeAggregationSource
	{
		public HistogramCompositeAggregationSourceDescriptor(string name) : base(name) {}

		int IHistogramCompositeAggregationSource.Interval { get; set; }

		public HistogramCompositeAggregationSourceDescriptor<T> Interval(int interval) =>
			Assign(a => a.Interval = interval);
	}
}
