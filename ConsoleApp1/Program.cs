// See https://aka.ms/new-console-template for more information

using Docker.DotNet;
using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

// var client = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine"))
    // .CreateClient();
// CreateContainerResponse response = new CreateContainerResponse();
// IContainer container = new ContainerBuilder()
//     .WithImage("mongo:latest")
//     .WithPortBinding(27017,27017)
//     .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(27017))
//     .Build();
// await StartMongoContainerAsync();
// await container.StartAsync();

Console.WriteLine("Hello, World!");

// await container.StopAsync();
// await StopMongoContainerAsync(response);

// async Task StartMongoContainerAsync()
// {
//   
//     // Pull the MongoDB image
//     await client.Images.CreateImageAsync(new ImagesCreateParameters
//     {
//         FromImage = "mongo",
//         Tag = "latest"
//     }, null, new Progress<JSONMessage>());
//
//     // Create the MongoDB container
//     response = await client.Containers.CreateContainerAsync(new CreateContainerParameters
//     {
//         Image = "mongo:latest",
//         ExposedPorts = new Dictionary<string, EmptyStruct>
//         {
//             { "27017", new EmptyStruct() }
//         },
//         HostConfig = new HostConfig
//         {
//             PortBindings = new Dictionary<string, IList<PortBinding>>
//             {
//                 { "27017", new List<PortBinding> { new PortBinding { HostPort = "27017" } } }
//             }
//         }
//     });
//
//     // Start the MongoDB container
//     await client.Containers.StartContainerAsync(response.ID, new ContainerStartParameters());
// }
//
// async Task StopMongoContainerAsync(CreateContainerResponse responseContainer)
// {
//     await client.Containers.StopContainerAsync(responseContainer.ID, new ContainerStopParameters());
//     await client.Containers.RemoveContainerAsync(responseContainer.ID, new ContainerRemoveParameters());
// }