DevLib.Mvc.Demo
===============

感谢 mahuidong （https://github.com/mahuidong） 提供演示程序
DevLib.Mvc的演示程序

一、目前用的数据库名是XXDB，如果你想用自己的数据库请进行如下操作：
1.右键项目XX.Data >> Entity FrameWork(没有此项的安装下即可,可baidu google) >> Reverse Enginner Code First
2.在打开的窗口中输入 连接的数据库串信息(如果是sql server 验证方式,需要记住密码)
3.点击确定 你所有的数据表就进入到XX.Data项目了
4.例如你的数据库名是 TestDB 那么就将整个解决方案的XXDB替换成TestDB,操作如下:VS>> 编辑>> 查找和替换>>在文件中替换(替换范围是整个解决方案)

二、XX.Web项目中有一个 上传图片的例子,上传图片需要配置下文件服务器，详细见ImageServer.config
其中 <server id="2" used="true" hostip="192.168.8.10" startdirname="ppd_img" username="administrator" password=""  serverurl="http://img01.pinpaide.cn"/>
used 是启用状态
hostip 文件服务器ip(当然也可以是你本机ip)
ppd_img 是共享目录名
username/password 是文件服务器的访问账户信息
serverurl 是ppd_img对应的目录 在公网的访问地址

三、整个解决方案不包含packages， 在生成解决方案的时候 会自动下载这些包

四、XX.Web 已经包含了 常用的操作实例，如:增、分页、上传等，详见 TestController 和 Views/Test/

五、XX.Web 项目包含了MVC URL重写的部分，详见App_Start/RouteConfig.cs

六、数据库在DB文件夹，附加上即可测试

七、该解决方案用的是VS2013开发，数据库用的sqlserver2008 r2, XX.Web用的MVC5
