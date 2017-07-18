namespace RaspberryBackend
{
    /// <summary>
    /// This Config is the central place for all Button Press related durations.
    /// </summary>
    public static class DurationConfig
    {
        public static short ShortPush = 50;
        public static short MediumPush = 500;
        public static short LongPush = 3000;

        public static int getDuration(string durationCategorie)
        {
            int duration;
            switch (durationCategorie)
            {
                case "Short":
                    duration = ShortPush;
                    break;
                case "Medium":
                    duration = MediumPush;
                    break;
                case "Long":
                    duration = LongPush;
                    break;
                default:
                    return -1;
            }

            return duration;
        }


    }
}
