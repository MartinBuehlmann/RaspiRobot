namespace DocumentStorage.FileBased;

using Autofac;

public class DocumentStorageFileBasedModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<FileStorage>()
            .As<IDocumentStorage>()
            .SingleInstance();
    }
}