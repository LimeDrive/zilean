using System.Text.Json.Serialization;

namespace Zilean.Shared.Features.Dmm;

public class ClearTorrentInfo
{
    [JsonPropertyName("raw_title")]
    public string? RawTitle { get; set; }

    [JsonPropertyName("parsed_title")]
    public string? ParsedTitle { get; set; }

    [JsonPropertyName("normalized_title")]
    public string? NormalizedTitle { get; set; }

    [JsonPropertyName("trash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Trash { get; set; }

    [JsonPropertyName("year")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Year { get; set; }

    [JsonPropertyName("resolution")]
    public string? Resolution { get; set; }

    [JsonPropertyName("seasons")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Seasons { get; set; }

    [JsonPropertyName("episodes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Episodes { get; set; }

    [JsonPropertyName("complete")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Complete { get; set; }

    [JsonPropertyName("volumes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int[]? Volumes { get; set; }

    [JsonPropertyName("languages")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Languages { get; set; }

    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    [JsonPropertyName("hdr")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Hdr { get; set; }

    [JsonPropertyName("codec")]
    public string? Codec { get; set; }

    [JsonPropertyName("audio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Audio { get; set; }

    [JsonPropertyName("channels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Channels { get; set; }

    [JsonPropertyName("dubbed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Dubbed { get; set; }

    [JsonPropertyName("subbed")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Subbed { get; set; }

    [JsonPropertyName("date")]
    public string? Date { get; set; }

    [JsonPropertyName("group")]
    public string? Group { get; set; }

    [JsonPropertyName("edition")]
    public string? Edition { get; set; }

    [JsonPropertyName("bit_depth")]
    public string? BitDepth { get; set; }

    [JsonPropertyName("bitrate")]
    public string? Bitrate { get; set; }

    [JsonPropertyName("network")]
    public string? Network { get; set; }

    [JsonPropertyName("extended")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Extended { get; set; }

    [JsonPropertyName("converted")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Converted { get; set; }

    [JsonPropertyName("hardcoded")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Hardcoded { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("ppv")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Ppv { get; set; }

    [JsonPropertyName("_3d")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Is3d { get; set; }

    [JsonPropertyName("site")]
    public string? Site { get; set; }

    [JsonPropertyName("size")]
    public string? Size { get; set; }

    [JsonPropertyName("proper")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Proper { get; set; }

    [JsonPropertyName("repack")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Repack { get; set; }

    [JsonPropertyName("retail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Retail { get; set; }

    [JsonPropertyName("upscaled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Upscaled { get; set; }

    [JsonPropertyName("remastered")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Remastered { get; set; }

    [JsonPropertyName("unrated")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Unrated { get; set; }

    [JsonPropertyName("documentary")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Documentary { get; set; }

    [JsonPropertyName("episode_code")]
    public string? EpisodeCode { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("container")]
    public string? Container { get; set; }

    [JsonPropertyName("extension")]
    public string? Extension { get; set; }

    [JsonPropertyName("torrent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool? Torrent { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = default!;

    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; set; }

    [JsonPropertyName("imdb")]
    public virtual ImdbFile? Imdb { get; set; }

    [JsonPropertyName("info_hash")]
    public string InfoHash { get; set; } = default!;

    public static ClearTorrentInfo FromTorrentInfo(TorrentInfo info)
    {
        return new ClearTorrentInfo
        {
            RawTitle = info.RawTitle,
            ParsedTitle = info.ParsedTitle,
            NormalizedTitle = info.NormalizedTitle,
            Trash = info.Trash == false ? null : info.Trash,
            Year = info.Year == 0 ? null : info.Year,
            Resolution = info.Resolution,
            Seasons = info.Seasons?.Length > 0 ? info.Seasons : null,
            Episodes = info.Episodes?.Length > 0 ? info.Episodes : null,
            Complete = info.Complete == false ? null : info.Complete,
            Volumes = info.Volumes?.Length > 0 ? info.Volumes : null,
            Languages = info.Languages?.Length > 0 ? info.Languages : null,
            Quality = info.Quality,
            Hdr = info.Hdr?.Length > 0 ? info.Hdr : null,
            Codec = info.Codec,
            Audio = info.Audio?.Length > 0 ? info.Audio : null,
            Channels = info.Channels?.Length > 0 ? info.Channels : null,
            Dubbed = info.Dubbed == false ? null : info.Dubbed,
            Subbed = info.Subbed == false ? null : info.Subbed,
            Date = info.Date,
            Group = info.Group,
            Edition = info.Edition,
            BitDepth = info.BitDepth,
            Bitrate = info.Bitrate,
            Network = info.Network,
            Extended = info.Extended == false ? null : info.Extended,
            Converted = info.Converted == false ? null : info.Converted,
            Hardcoded = info.Hardcoded == false ? null : info.Hardcoded,
            Region = info.Region,
            Ppv = info.Ppv == false ? null : info.Ppv,
            Is3d = info.Is3d == false ? null : info.Is3d,
            Site = info.Site,
            Size = info.Size,
            Proper = info.Proper == false ? null : info.Proper,
            Repack = info.Repack == false ? null : info.Repack,
            Retail = info.Retail == false ? null : info.Retail,
            Upscaled = info.Upscaled == false ? null : info.Upscaled,
            Remastered = info.Remastered == false ? null : info.Remastered,
            Unrated = info.Unrated == false ? null : info.Unrated,
            Documentary = info.Documentary == false ? null : info.Documentary,
            EpisodeCode = info.EpisodeCode,
            Country = info.Country,
            Container = info.Container,
            Extension = info.Extension,
            Torrent = info.Torrent == false ? null : info.Torrent,
            Category = info.Category,
            ImdbId = info.ImdbId,
            Imdb = info.Imdb,
            InfoHash = info.InfoHash
        };
    }
}