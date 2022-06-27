Assets/BundleEdting
一些编辑性的、需要配合编译过程的美术资源。比如UI在保存时自动导出Prefab到Assets/BundleResources目录下
Model
//TODO  打包
Assets/BundleResources
Asset Bundle全自动化导出的关键，此目录下所有的文件都会设置上abName，然后进行导出，并使用Unity管理依赖。KSFramework约定这几个目录：角色，特效，shader，声音中的prefab也会设置abName，在生成ab时一并导出。。
