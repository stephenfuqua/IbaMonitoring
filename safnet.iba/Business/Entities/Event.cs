using System;

namespace safnet.iba.Business.Entities
{
    /// <summary>
    /// Abstract class modeling something that occurs at a Location at a specific Timepoint and with a fixed duration.
    /// </summary>
    public abstract class Event : SafnetBaseEntity
    {
        private DateTime? _startTime;
        private DateTime? _endTime;

        /// <summary>
        /// Gets or sets the starting date and time for an Event.
        /// </summary>
        public DateTime? StartTimeStamp
        {
            get
            {
                return _startTime;
            }
            set
            {
                if (_endTime == null || value < _endTime)
                {
                    _startTime = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Start time must be less than end time");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ending date and time for an Event, constrained to be after the StartTimeStamp.
        /// </summary>
        public DateTime? EndTimeStamp 
        {
            get
            {
                return _endTime;
            }
            set
            {
                if (_startTime == null || value > _startTime)
                {
                    _endTime = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("End time must be greater than start time");
                }
            }
        }

        /// <summary>
        /// Gets the duration between the ending and starting times.
        /// </summary>
        public TimeSpan? Duration
        {
            get
            {
                if (_endTime == null || _startTime == null)
                {
                    return null;
                }
                else
                {
                    return _endTime.Value.Subtract(_startTime.Value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a particular location Id for the Event.
        /// </summary>
        public Guid LocationId { get; set; }
    }
}
