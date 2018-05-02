using System;
using Newtonsoft.Json;

namespace Nest
{

	public interface ITermsCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		/// A script to create the values for the composite buckets
		/// </summary>
		[JsonProperty("script")]
		IScript Script { get; set; }
	}

	public class TermsCompositeAggregationSource : CompositeAggregationSourceBase, ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSource(string name) : base(name) {}

		/// <inheritdoc />
		public IScript Script { get; set; }
	}

	public class TermsCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<TermsCompositeAggregationSourceDescriptor<T>, ITermsCompositeAggregationSource, T>, ITermsCompositeAggregationSource
	{
		public TermsCompositeAggregationSourceDescriptor(string name) : base(name) {}

		IScript ITermsCompositeAggregationSource.Script { get; set; }

		public TermsCompositeAggregationSourceDescriptor<T> Script(Func<ScriptDescriptor, IScript> selector) =>
			Assign(a => a.Script = selector?.Invoke(new ScriptDescriptor()));
	}
}
