using System;
using System.Data;
using Dapper;

namespace festivalprojekt.Server.Models
{
	public class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
	{
		public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
		{
			parameter.Value = value.ToOffset(TimeSpan.Zero).ToString("dd-MMM-yyyy"+"T"+"HH:mm:ss K");
			parameter.DbType = DbType.String;
		}
		public override DateTimeOffset Parse(object value)
		{
			switch (value)
			{
				case DateTime time:
					return new DateTimeOffset(DateTime.SpecifyKind(time, DateTimeKind.Utc), TimeSpan.Zero);
				case DateTimeOffset dto:
					return dto;
				default:
					throw new InvalidOperationException("Must be DateTime or DateTimeOffset object to be mapped.");
			}
		}
	}
}

