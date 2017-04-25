# protobuf-gen-plugin
protobuf-gen-plugin

基于C#的 Google Protobuf 代码生成工具
基于插件的机制开发，程序自动扫描同一目录下所有DLL，发现查找所有继承IPlugin接口的类，依次交由这些插件执行，来达到代码生成的目录
