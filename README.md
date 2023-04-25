# ZhoBot: Making Video and GIF Sharing Easy on Discord

<div id="header" align="center">
  <a href="https://discord.com/api/oauth2/authorize?client_id=1089819594211987510&permissions=412317181952&scope=bot%20applications.commands">
      <img src="https://i.postimg.cc/13LctGh4/426c7b7f-fc19-42bc-9a81-61f1dcf0a31a-transformed.png" width="300"/>
    </a>
  <div id="badges">
    <a href="https://discord.com/api/oauth2/authorize?client_id=1089819594211987510&permissions=412317181952&scope=bot%20applications.commands">
      <img src="https://img.shields.io/badge/invite_bot_to_your_server-blue?style=for-the-badge"/>
    </a>
  </div>
</div>

## Improve Your Discord Experience

### 🚀 Meet ZhoBot

ZhoBot is a helpful and easy-to-use bot created to make sharing videos and GIFs on Discord a breeze. The bot removes the need to download content from different platforms, saving you time and effort. With ZhoBot, you can send videos and GIFs from many sources without downloading them to your device, making the sharing process quick and efficient.
![Sharing Video Example](./assets/sharing-video-example.gif)

ZhoBot supports all the most popular video hosting and social networks like YouTube, Vimeo, and TikTok, making it incredibly easy to share content from almost any source. However, please note that while ZhoBot strives to support as many sources as possible, there may be instances where a particular source isn't supported. Additionally, Discord's own file size limits still apply.

ZhoBot is designed to adapt files to various server settings and restrictions such as upload limit or unsupported video codecs. ZhoBot is a truly versatile and handy companion for all your video and GIF sharing needs.

<div id="header" align="center">
  <img src="https://s12.gifyu.com/images/diff2.gif" width="500"/>
</div>

### 🤖 Bot Commands

To help you get started with ZhoBot, here's a list of available commands:

- `/help` : This command displays the list of commands available for ZhoBot.
- `/info`: Shows information about the bot and its creator.
- `/check-download-limit`: Checks the maximum attachment size allowed on the current server.
- `/post-video-by-url`: Posts a video by entering a URL. The user can also add a message and set a filename. Supports numerous sources, but not all, and download size limits apply.
- `/post-gif-by-url`: Similar to the post-video-by-url command, but only supports direct URLs to GIF files.


## Explore the Technical Aspects

### Built with .NET 7 Framework

ZhoBot is constructed using the powerful and reliable **.NET 7** and **ASP.NET Core 7.0** frameworks. These modern and popular technologies are the top choices for creating scalable and high-performance web applications, ensuring that ZhoBot can efficiently handle various tasks and provide an excellent user experience.

The project is fully asynchronous, allowing it to manage multiple requests at the same time. This design ensures that ZhoBot can serve many users without sacrificing performance.

### Libraries and Tools

ZhoBot uses several libraries and tools to provide its features:

- [Discord .NET API](https://discordnet.dev/): Used to interact with Discord's API, enabling the bot to work with Discord services and manage user interactions.
- [yt-dlp](https://github.com/yt-dlp/yt-dlp): A command-line program for downloading videos from various sources, simplifying the process for sources where HTTP downloading does not work.
- [FFmpeg](https://www.ffmpeg.org/): A tool for processing and transcoding video files, ensuring compatibility with Discord's supported codecs.
- [ffprobe](https://ffmpeg.org/ffprobe.html): A multimedia streaming analyzer, used to determine video codecs for better processing.
- [Microsoft.Extensions.DependencyInjection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0): A lightweight built-in dependency injection container, used to manage the relationships between ZhoBot components and improve the app's testability.
- [xUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq4): xUnit is a testing tool for .NET applications, while Moq is a popular .NET mocking library for creating mock objects during unit testing. Together, they contribute to about 150 unit and integration test cases, ensuring the correct behavior of ZhoBot's components during testing scenarios.
- [Serilog](https://serilog.net/): A flexible logging framework, helping to track and diagnose potential issues in ZhoBot.
- [OpenAI's ChatGPT-4](https://openai.com/product/gpt-4): Used for explanations of new topics and generation of various texts, such as this readme, enhancing the project's documentation and user experience.

### Project Structure

ZhoBot's structure includes a configurator and three separate services:

1. **Configurator**: Sets up dependencies, configures logging, and initializes the bot.
2. **Discord API Service**: Responsible for bot authorization, launch, and user command handling.
3. **Download Service**: Manages file operations, including downloading, storing, and deleting files on demand.
4. **Tools Service**: Generates requests and runs third-party applications like yt-dlp, FFmpeg, and ffprobe for video information gathering, downloading, and processing.

**Services communicate using Dependency Injection**, ensuring a clean and modular architecture. This approach allows ZhoBot components to work together efficiently and makes it easier to maintain and extend the project in the future.

### ZhoBot's Limitations: Opportunities for Growth

ZhoBot is a highly functional and valuable tool, but it's important to recognize that there are areas with room for improvement. These limitations present opportunities for growth, and we encourage the community to contribute to addressing them in future updates.

1. **Expanding Supported Sources:** Although ZhoBot supports a wide variety of video and GIF sources, there may be instances where a specific source isn't compatible. Continuously working to extend the range of supported sources is a priority, and community contributions are greatly appreciated.

2. **Managing File Size Limits:** ZhoBot is subject to Discord's own file size limitations, which means that sharing extremely large videos or GIFs might not be possible through the bot. Exploring ways to optimize and compress files without sacrificing quality could be an area for further research and development.

3. **Addressing Potential Performance Issues:** As ZhoBot's user base expands, it might encounter performance bottlenecks or challenges in processing an increased number of requests. The bot has not been tested with a large number of requests, and the current server is relatively slow and small. Developing scalable solutions, optimizing the codebase, and improving server capabilities to handle high demand are crucial to ensure that ZhoBot remains a reliable tool for all users.

4. **Mitigating Dependency on External Services:** ZhoBot depends on external services like yt-dlp for its core functionality. If these services experience downtime or issues, ZhoBot's performance could be impacted. Implementing redundancy and fallback options for such situations could help enhance the bot's resilience.

6. **Enhancing Code Architecture:** While the creator has applied the best practices known to him, he acknowledges that there are areas where he is not good enough and remembers what can be done better but does not yet have the necessary knowledge. Refining the code architecture can lead to more efficient performance and easier maintenance, ultimately contributing to a better overall experience with ZhoBot. Exploring new best practices and implementing design patterns tailored to the project's needs are essential steps in this ongoing process.

6. **Broadening the Command Set:** While ZhoBot already offers a variety of commands, users may desire additional features. Gathering community input to identify and prioritize new commands and functionalities is essential for helping ZhoBot evolve and better serve its users.

By identifying these areas for growth, further improvements can be made to ZhoBot's capabilities, enhancing the file sharing experience on Discord.
#

#### Creator Contact Information
- Name: Boris Zhuravel
- Email: [borisjuravel@gmail.com](mailto:borisjuravel@gmail.com)
- Telegram: https://t.me/zhuboris