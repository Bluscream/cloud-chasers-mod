namespace StormChasers {
    public static class Extensions {
        public static string getName(this Player player) => player.photonView.owner.NickName;
        public static string str(this Player player) => $@"
Name: {player.name}
playerMapNameText.text: {player.playerMapNameText.text}
playerNameLabelText.text: {player.playerNameLabelText.text}
owner.NickName: {player.photonView.owner.NickName}
Position: {player.transform.position}
Rotation: {player.transform.rotation}";
        public static string getName(this CarTornado truck) => truck.name;
        public static string getOwnerName(this CarTornado truck) => truck.photonView.owner.NickName;
        public static string str(this CarTornado truck) => $@"
Name: {truck.name}
playersNameLabelText: {truck.playersNameLabelText}
owner.NickName: {truck.photonView.owner.NickName}
Position: {truck.transform.position}
Rotation: {truck.transform.rotation}
Speed: {truck.getSpeed()} ({truck.getSpeedInKMH()})";
    }
}
