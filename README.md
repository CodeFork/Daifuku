![Daifuku](https://raw.githubusercontent.com/goto10hq/Daifuku/master/daifuku-icon.png)

# Daifuku

[![Software License](https://img.shields.io/badge/license-MIT-brightgreen.svg?style=flat-square)](LICENSE.md)
[![Latest Version on NuGet](https://img.shields.io/nuget/v/Daifuku.svg?style=flat-square)](https://www.nuget.org/packages/Daifuku/)
[![NuGet](https://img.shields.io/nuget/dt/Daifuku.svg?style=flat-square)](https://www.nuget.org/packages/Daifuku/)
[![Visual Studio Team services](https://img.shields.io/vso/build/frohikey/c3964e53-4bf3-417a-a96e-661031ef862f/124.svg?style=flat-square)](https://github.com/goto10hq/Daifuku)
[![.NETStandard 2.0](https://img.shields.io/badge/.NETStandard-2.0-blue.svg)](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md)

## What Daifuku can do?

### Configure the HTTP request pipeline

```csharp
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
  // set Server
  app.UseServerHeader();

  // set Powered by
  app.UsePoweredBy();

  // set No Mime Sniff
  app.UseNoMimeSniff();

  // set Referrer policy
  app.UseReferrerPolicy(ReferrerPolicy.NoReferrer);

  // set Frame guard
  app.UseFrameGuard(new FrameGuardOptions(FrameGuard.SameOrigin));

  // set XSS protection
  app.UseXssProtection(XssProtection.EnabledWithBlock);

  // or just forget all settings and use default pipeline :)
  app.UseDaifuku();

  // pipeline stuff below is not set in UseDaifuku
  // ---------------------------------------------

  // do we use HTTPS?
  //var options = new RewriteOptions().AddRedirectToHttpsPermanent();
  //app.UseRewriter(options);
  app.UseHsts();

  // configure domain redirects
  app.RedirectDomains(new Dictionary<string, string>
  {
      { "daifu.ku", "www.daifu.ku" },
      { "test.azurewebsites.net", "www.daifu.ku" },
  });

  // set custom header
  app.UseCustomHeader("X-Overlord", "Daifuku");

  // set content security policy
  app.UseContentSecurityPolicy(
    new ContentSecurityPolicyBuilder()
    .WithDefaultSource(CspConstants.Self)
    .WithImageSource("http://blobs.daifu.ku")
    .WithFontSource(CspConstants.Self)
    .WithFrameAncestors(CspConstants.None)
    .WithMediaSource(CspConstants.Schemes.MediaStream)
    .BuildPolicy());

  // set Expect CT
  app.UseExpectCt(86400, "https://daifu.ku/report");
```

## IIS

As long as IIS injects some headers you can clean up headers configuring your ``web.config``

```xml
<configuration>
  <system.webServer>
    <httpProtocol>
      <customHeaders>
        <clear />
      </customHeaders>
      <redirectHeaders>
        <clear />
      </redirectHeaders>
    </httpProtocol>
  </system.webServer>
</configuration>
```

## External documents

[HTTP headers](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers) @ MDN web docs moz://a

## Acknowledgement

[HardHat](https://github.com/TerribleDev/HardHat) by [TerribleDev](https://github.com/TerribleDev)

## License

MIT © [frohikey](http://frohikey.com) / [Goto10 s.r.o.](http://www.goto10.cz)
