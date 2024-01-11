Assets/BundleEdting
一些编辑性的、需要配合编译过程的美术资源。比如UI在保存时自动导出Prefab到Assets/BundleResources目录下
Model
//TODO  打包
Assets/BundleResources
Asset Bundle全自动化导出的关键，此目录下所有的文件都会设置上abName，然后进行导出，并使用Unity管理依赖。KSFramework约定这几个目录：角色，特效，shader，声音中的prefab也会设置abName，在生成ab时一并导出。。


### UI层次
选择 UIType，UIType用于设置UI之间的遮挡关系，这个还可以在创建好的预设里再改。
UIType 分为 5种 GameUI,MainUI,NormalUI,HeadInfoUI,TipsUI
　GameUI在UI分层中的最底层，一般用于游戏里面的血条，浮动UI等。
　MainUI在GameUI层之上，一般用于常驻的UI如主城UI。
　NormalUI在MainUI之上，一般用于普通UI，例如商店。
　HeadInfoUI在NormalUI之上 ，一般用于常驻的置顶显示的UI,例如体力和金钱UI。
　TipsUI在最上层，一般用来显示弹窗

### UI事件
