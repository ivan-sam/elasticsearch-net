using Newtonsoft.Json;

namespace Nest
{
	public interface IDateHistogramCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("interval")]
		Union<DateInterval, Time> Interval { get; set; }

		/// <summary>
		///
		/// </summary>
		[JsonProperty("time_zone")]
		string Timezone { get; set; }
	}

	public class DateHistogramCompositeAggregationSource : CompositeAggregationSourceBase, IDateHistogramCompositeAggregationSource
	{
		public DateHistogramCompositeAggregationSource(string name) : base(name) {}

		/// <inheritdoc />
		public Union<DateInterval,Time> Interval { get; set; }

		/// <inheritdoc />
		public string Timezone { get; set; }
	}

	public class DateHistogramCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource, T>,
			IDateHistogramCompositeAggregationSource
	{
		public DateHistogramCompositeAggregationSourceDescriptor(string name) : base(name) {}

		Union<DateInterval,Time> IDateHistogramCompositeAggregationSource.Interval { get; set; }
		string IDateHistogramCompositeAggregationSource.Timezone { get; set; }

		public DateHistogramCompositeAggregationSourceDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramCompositeAggregationSourceDescriptor<T> Interval(Time interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramCompositeAggregationSourceDescriptor<T> Timezone(string timezone) =>
			Assign(a => a.Timezone = timezone);
	}
}
