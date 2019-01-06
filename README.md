# protobuf-gen-plugin
----
**注意：请不要使用该类库，已弃用，请使用protoc-gen-dotbpe 替代**
基于C#的 Google Protobuf 代码生成工具
基于插件的机制开发，程序自动扫描plugins目录，发现查找所有继承IPlugin接口的类，依次交由这些插件执行，来达到代码生成的目录

## 使用方法
1. 下载dist目录中的ProtobufGen.rar文件并解压
2. `Install-Package Protobuf.Gen.Core` 下载依赖的DLL
3. 实现其中的IPlugin接口，编译成DLL，并copy到步骤1中的plugins目录下
4. 编写.proto文件
5. 使用protoc命令配合生成需要的代码，如下shell代码所示：注意protoc-gen-**dotbpe** 和 --**dotbpe**_out 名字必须一致 可以改成自己的命名


```shell
set -ex

cd $(dirname $0)/../../sample/HelloRpc/

PROTOC=protoc
PLUGIN=protoc-gen-dotbpe=../../tool/ampplugin/Protobuf.Gen.exe
HELLORPC_DIR=./HelloRpc.Common/
PROTO_DIR=../../protos

$PROTOC  -I=$PROTO_DIR --csharp_out=$HELLORPC_DIR --dotbpe_out=$HELLORPC_DIR \
    $PROTO_DIR/{dotbpe_option,hello_rpc}.proto  --plugin=$PLUGIN

```

具体可参考[protoc](https://github.com/google/protobuf)的使用方法 



插件编写可参考目录https://github.com/xuanye/protobuf-gen-csharp/tree/master/src/Protobuf.Gen.Amp 中已有的实现

使用方法可参考 DotBPE中的使用https://github.com/xuanye/dotbpe
