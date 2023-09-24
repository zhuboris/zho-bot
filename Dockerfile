FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

RUN apt-get update && \
    apt-get install -y apt-transport-https ca-certificates curl software-properties-common wget xz-utils git

RUN git clone https://github.com/asborisjuas/zho-bot ./
COPY discordtoken.json ./ZhoBotDiscord/discordtoken.json

RUN dotnet restore
RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /source/out ./

RUN apt-get update && \
    apt-get install -y python3 python3-pip wget && \
    python3 -m pip install yt-dlp==2023.3.4 xattr && \
    apt-get install -y atomicparsley && \
    wget -O ffmpeg.tar.xz https://github.com/yt-dlp/FFmpeg-Builds/releases/download/latest/ffmpeg-master-latest-linux64-gpl.tar.xz && \
    tar -xf ffmpeg.tar.xz && \
    mv ffmpeg-*/bin/ffmpeg /usr/local/bin/ffmpeg && \
    mv ffmpeg-*/bin/ffprobe /usr/local/bin/ffprobe && \
    rm -rf ffmpeg.tar.xz ffmpeg-*-gpl && \
    chmod a+rx /usr/local/bin/ffmpeg && \
    chmod a+rx /usr/local/bin/ffprobe && \
    rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "AppConfigurator.dll"]
