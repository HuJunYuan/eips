/**
 * jQuery EasyUI 1.5
 * 
 * Copyright (c) 2009-2017 www.coreland.com.cn. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
/**
 * richtextbox - jQuery EasyUI
 * 
 * Dependencies:
 * 	 富文本输入框
 * 业务参数：
   
 * 依赖项：
 *   1、jquery.jdirk.js
 *   2、ueditor.all.min.js
 *   3、App.js
 */
$.parser.plugins.push("richtextbox");

(function ($) {

    var TOOLBARS = [
        // 全量工具集
        [[
            'fullscreen', 'source', '|', 'undo', 'redo', '|',
            'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'removeformat', 'formatmatch', 'autotypeset', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', 'cleardoc', '|',
            'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',
            'customstyle', 'paragraph', 'fontfamily', 'fontsize', '|',
            'directionalityltr', 'directionalityrtl', 'indent', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|',
            'link', 'unlink', 'anchor', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
            'simpleupload', 'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'insertframe', 'insertcode', 'pagebreak', 'template', 'background', '|',
            'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage', '|',
            'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
            'print', 'preview', 'searchreplace', 'drafts', 'help'
        ]],
        // 常用工具集
        [[
            'fullscreen', 'undo', 'redo', 'removeformat', 'formatmatch', 'autotypeset', 'cleardoc', '|',
            'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', '|',
            'rowspacingtop', 'rowspacingbottom', 'lineheight', '|',
            'paragraph', 'fontfamily', 'fontsize', '|',
            'directionalityltr', 'directionalityrtl', 'indent', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|', 'touppercase', 'tolowercase', '|',
            'link', 'unlink', '|', 'imagenone', 'imageleft', 'imageright', 'imagecenter', '|',
            'simpleupload', 'insertimage', 'emotion', 'scrawl', 'insertvideo', 'music', 'attachment', 'map', 'insertframe', 'pagebreak', 'template', 'background', '|',
            'horizontal', 'date', 'time', 'spechars', 'snapscreen', 'wordimage', '|',
            'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
            'print', 'preview', 'searchreplace'
        ]],
        // 简单工具集
        [[
            'removeformat', 'formatmatch', 'cleardoc', '|',
            'bold', 'italic', 'underline', 'fontborder', 'strikethrough', 'superscript', 'subscript', 'blockquote', 'pasteplain', '|', 'forecolor', 'backcolor', 'insertorderedlist', 'insertunorderedlist', 'selectall', '|',
            'paragraph', 'fontfamily', 'fontsize', '|',
            'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
            'link', 'unlink', '|',
            'simpleupload', 'insertimage', 'emotion', 'insertvideo', 'music','|',
            'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols', 'charts', '|',
            'preview', 'searchreplace'
        ]],
        // 极简工具集
        [["fontfamily", "fontsize", "bold", "italic", "underline", "forecolor", "backcolor", "insertorderedlist", "insertunorderedlist"]]
    ];



    function init(target) {
        var state = $.data(target, 'richtextbox'),
            opts = state.options,
            t = $(target);

        if (t.has(".easyui-richtextbox").length == 0) {
            t.addClass("easyui-richtextbox");
        }
        if (!state.ue) {
            UE.removeInstance(opts.id);
        }
        state.ue = UE.getEditor(opts.id, opts);

        showLabel(target);
        return t;
    }

    function getUEBasePath() {

        return $.util.hostPath + "/EIP.Web/Scripts/ueditor/";

    }

    function showLabel(target) {
        var state = $.data(target, "richtextbox");
        var options = state.options;
        var ue = state.ue;
        if (options.label) {
            if (typeof options.label == "object") {
                state.label = $(options.label);
                state.label.attr("for", options.id);
            } else {
                $(state.label).remove();
                state.label = $("<label class=\"textbox-label\"></label>").html(options.label);
                state.label.css("textAlign", options.labelAlign).attr("for", options.id);
                state.label.insertBefore($("#"+options.id)[0]);
                state.label.removeClass("textbox-label-left textbox-label-right textbox-label-top");
                state.label.addClass("textbox-label-top");
            }
        } else {
            $(state.label).remove();
        }

    }

    function validate(target) {
        var state = $.data(target, "richtextbox"),
            opts = state.options,
            ue = state.ue,
            t = $(target).prev();

        if (opts.required && ue.getContentTxt() == "") {
            t.addClass("validatebox-invalid");
            ue.focus();
            App.showMessageBox("富文本内容不能为空",2);
        } else {
            t.removeClass("validatebox-invalid");
        }
    }

    $.fn.richtextbox = function (options, param) {
        if (typeof options == 'string') {
            return $.fn.richtextbox.methods[options](this.next(), param);
        }

        options = options || {};
        return this.each(function () {
            var state = $.data(this, 'richtextbox');
            if (state) {
                $.extend(state.options, options);
            } else {
                $.fn.richtextbox.defaults.toolbars = [[]];
                options = $.extend({}, $.fn.richtextbox.defaults, $.fn.richtextbox.parseOptions(this), options);
                if (options.toolbarType != -1) {
                    var toolbars = [];
                    Array.copyTo(TOOLBARS[options.toolbarType][0], toolbars);
                    Array.copyTo(options.toolbars[0], toolbars);
                    options.toolbars[0] = toolbars;
                }
                $.data(this, 'richtextbox', { options: options});
            }
            init(this);
        });
    };

    $.fn.richtextbox.methods = {
        options: function (jq) {
            return $.data(jq[0], 'richtextbox').options;
        },
        validate: function (jq) {
            return validate(jq[0]);
        },
        setHeight: function (jq,height) {
            var state = $.data(jq[0], 'richtextbox');
            state.ue.setHeight();
        },
        getContent: function (jq,cmd, fn, notSetCursor, ignoreBlank, formatter) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.getContent(cmd, fn, notSetCursor, ignoreBlank, formatter);
        },
        getAllHtml: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.getAllHtml();
        },
        getPlainTxt: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.getPlainTxt();
        },
        getText: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.getContentTxt();
        },
        setContent: function (jq, html, isAppendTo, notFireSelectionchange) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.setContent(html, isAppendTo, notFireSelectionchange);
        },
        focus: function (jq, toEnd) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.focus(toEnd);
        },
        isFocus: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.isFocus();
        },
        blur: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.blur();
        },
        hasContents: function (jq, tags) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.hasContents(tags);
        },
        reset: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.reset();
        },
        enable: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.enable();
        },
        disable: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.disable();
        },
        show: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.show();
        },
        hide: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.hide();
        },
        destroy: function (jq) {
            var state = $.data(jq[0], 'richtextbox');
            if (state && state.ue) state.ue.destroy();
            if (state && state.label) $(state.label).remove();
        },
        getContentLength: function (jq, ingoneHtml, tagNames) {
            var state = $.data(jq[0], 'richtextbox');
            return state.ue.getContentLength(ingoneHtml, tagNames);
        }
    };

    $.fn.richtextbox.parseOptions = function (target) {
        return $.extend({}, $.parser.parseOptions(target, ['id', 'name']));
    };

    var URL = window.UEDITOR_HOME_URL || getUEBasePath();
    $.fn.richtextbox.defaults = {
        //为编辑器实例添加一个路径，这个不能被注释
        UEDITOR_HOME_URL: URL

        // 服务器统一请求接口路径
        , serverUrl: URL + "net/controller.ashx"

        // -1:不设置，0:全量工具集，1:常用工具集，2:简单工具集，3:极简工具集
        , toolbarType: 1

        //工具栏上的所有的功能按钮和下拉框，可以在new编辑器的实例时选择自己需要的重新定义
        , toolbars: [[]]
        //当鼠标放在工具栏上时显示的tooltip提示,留空支持自动多语言配置，否则以配置值为准
        //,labelMap:{
        //    'anchor':'', 'undo':''
        //}

        //语言配置项,默认是zh-cn。有需要的话也可以使用如下这样的方式来自动多语言切换，当然，前提条件是lang文件夹下存在对应的语言文件：
        //lang值也可以通过自动获取 (navigator.language||navigator.browserLanguage ||navigator.userLanguage).toLowerCase()
        //,lang:"zh-cn"
        //,langPath:URL +"lang/"

        //主题配置项,默认是default。有需要的话也可以使用如下这样的方式来自动多主题切换，当然，前提条件是themes文件夹下存在对应的主题文件：
        //现有如下皮肤:default
        //,theme:'default'
        //,themePath:URL +"themes/"

        //,zIndex : 900     //编辑器层级的基数,默认是900

        //针对getAllHtml方法，会在对应的head标签中增加该编码设置。
        //,charset:"utf-8"

        //若实例化编辑器的页面手动修改的domain，此处需要设置为true
        //,customDomain:false

        //常用配置项目
        //,isShow : true    //默认显示编辑器

        //,textarea:'editorValue' // 提交表单时，服务器获取编辑器提交内容的所用的参数，多实例时可以给容器name属性，会将name给定的值最为每个实例的键值，不用每次实例化的时候都设置这个值

        //,initialContent:'欢迎使用ueditor!'    //初始化编辑器的内容,也可以通过textarea/script给值，看官网例子

        //,autoClearinitialContent:true //是否自动清除编辑器初始内容，注意：如果focus属性设置为true,这个也为真，那么编辑器一上来就会触发导致初始化的内容看不到了

        //,focus:false //初始化时，是否让编辑器获得焦点true或false

        //如果自定义，最好给p标签如下的行高，要不输入中文时，会有跳动感
        //,initialStyle:'p{line-height:1em}'//编辑器层级的基数,可以用来改变字体等

        //,iframeCssUrl: URL + '/themes/iframe.css' //给编辑区域的iframe引入一个css文件

        //indentValue
        //首行缩进距离,默认是2em
        //,indentValue:'2em'

        //,initialFrameWidth:1000  //初始化编辑器宽度,默认1000
        //,initialFrameHeight:320  //初始化编辑器高度,默认320

        //,readonly : false //编辑器初始化结束后,编辑区域是否是只读的，默认是false

        //,autoClearEmptyNode : true //getContent时，是否删除空的inlineElement节点（包括嵌套的情况）

        //启用自动保存
        //,enableAutoSave: true
        //自动保存间隔时间， 单位ms
        //,saveInterval: 500

        //,fullscreen : false //是否开启初始化时即全屏，默认关闭

        //,imagePopup:true      //图片操作的浮层开关，默认打开

        //,autoSyncData:true //自动同步编辑器要提交的数据
        //,emotionLocalization:false //是否开启表情本地化，默认关闭。若要开启请确保emotion文件夹下包含官网提供的images表情文件夹

        //粘贴只保留标签，去除标签所有属性
        //,retainOnlyLabelPasted: false

        //,pasteplain:false  //是否默认为纯文本粘贴。false为不使用纯文本粘贴，true为使用纯文本粘贴
        //纯文本粘贴模式下的过滤规则
        //'filterTxtRules' : function(){
        //    function transP(node){
        //        node.tagName = 'p';
        //        node.setStyle();
        //    }
        //    return {
        //        //直接删除及其字节点内容
        //        '-' : 'script style object iframe embed input select',
        //        'p': {$:{}},
        //        'br':{$:{}},
        //        'div':{'$':{}},
        //        'li':{'$':{}},
        //        'caption':transP,
        //        'th':transP,
        //        'tr':transP,
        //        'h1':transP,'h2':transP,'h3':transP,'h4':transP,'h5':transP,'h6':transP,
        //        'td':function(node){
        //            //没有内容的td直接删掉
        //            var txt = !!node.innerText();
        //            if(txt){
        //                node.parentNode.insertAfter(UE.uNode.createText(' &nbsp; &nbsp;'),node);
        //            }
        //            node.parentNode.removeChild(node,node.innerText())
        //        }
        //    }
        //}()

        //,allHtmlEnabled:false //提交到后台的数据是否包含整个html字符串

        //insertorderedlist
        //有序列表的下拉配置,值留空时支持多语言自动识别，若配置值，则以此值为准
        //,'insertorderedlist':{
        //      //自定的样式
        //        'num':'1,2,3...',
        //        'num1':'1),2),3)...',
        //        'num2':'(1),(2),(3)...',
        //        'cn':'一,二,三....',
        //        'cn1':'一),二),三)....',
        //        'cn2':'(一),(二),(三)....',
        //     //系统自带
        //     'decimal' : '' ,         //'1,2,3...'
        //     'lower-alpha' : '' ,    // 'a,b,c...'
        //     'lower-roman' : '' ,    //'i,ii,iii...'
        //     'upper-alpha' : '' , lang   //'A,B,C'
        //     'upper-roman' : ''      //'I,II,III...'
        //}

        //insertunorderedlist
        //无序列表的下拉配置，值留空时支持多语言自动识别，若配置值，则以此值为准
        //,insertunorderedlist : { //自定的样式
        //    'dash' :'— 破折号', //-破折号
        //    'dot':' 。 小圆圈', //系统自带
        //    'circle' : '',  // '○ 小圆圈'
        //    'disc' : '',    // '● 小圆点'
        //    'square' : ''   //'■ 小方块'
        //}
        //,listDefaultPaddingLeft : '30'//默认的左边缩进的基数倍
        //,listiconpath : 'http://bs.baidu.com/listicon/'//自定义标号的路径
        //,maxListLevel : 3 //限制可以tab的级数, 设置-1为不限制

        //,autoTransWordToList:false  //禁止word中粘贴进来的列表自动变成列表标签

        //fontfamily
        //字体设置 label留空支持多语言自动切换，若配置，则以配置值为准
        //,'fontfamily':[
        //    { label:'',name:'songti',val:'宋体,SimSun'},
        //    { label:'',name:'kaiti',val:'楷体,楷体_GB2312, SimKai'},
        //    { label:'',name:'yahei',val:'微软雅黑,Microsoft YaHei'},
        //    { label:'',name:'heiti',val:'黑体, SimHei'},
        //    { label:'',name:'lishu',val:'隶书, SimLi'},
        //    { label:'',name:'andaleMono',val:'andale mono'},
        //    { label:'',name:'arial',val:'arial, helvetica,sans-serif'},
        //    { label:'',name:'arialBlack',val:'arial black,avant garde'},
        //    { label:'',name:'comicSansMs',val:'comic sans ms'},
        //    { label:'',name:'impact',val:'impact,chicago'},
        //    { label:'',name:'timesNewRoman',val:'times new roman'}
        //]

        //fontsize
        //字号
        //,'fontsize':[10, 11, 12, 14, 16, 18, 20, 24, 36]

        //paragraph
        //段落格式 值留空时支持多语言自动识别，若配置，则以配置值为准
        //,'paragraph':{'p':'', 'h1':'', 'h2':'', 'h3':'', 'h4':'', 'h5':'', 'h6':''}

        //rowspacingtop
        //段间距 值和显示的名字相同
        //,'rowspacingtop':['5', '10', '15', '20', '25']

        //rowspacingBottom
        //段间距 值和显示的名字相同
        //,'rowspacingbottom':['5', '10', '15', '20', '25']

        //lineheight
        //行内间距 值和显示的名字相同
        //,'lineheight':['1', '1.5','1.75','2', '3', '4', '5']

        //customstyle
        //自定义样式，不支持国际化，此处配置值即可最后显示值
        //block的元素是依据设置段落的逻辑设置的，inline的元素依据BIU的逻辑设置
        //尽量使用一些常用的标签
        //参数说明
        //tag 使用的标签名字
        //label 显示的名字也是用来标识不同类型的标识符，注意这个值每个要不同，
        //style 添加的样式
        //每一个对象就是一个自定义的样式
        //,'customstyle':[
        //    {tag:'h1', name:'tc', label:'', style:'border-bottom:#ccc 2px solid;padding:0 4px 0 0;text-align:center;margin:0 0 20px 0;'},
        //    {tag:'h1', name:'tl',label:'', style:'border-bottom:#ccc 2px solid;padding:0 4px 0 0;margin:0 0 10px 0;'},
        //    {tag:'span',name:'im', label:'', style:'font-style:italic;font-weight:bold'},
        //    {tag:'span',name:'hi', label:'', style:'font-style:italic;font-weight:bold;color:rgb(51, 153, 204)'}
        //]

        //打开右键菜单功能
        //,enableContextMenu: true
        //右键菜单的内容，可以参考plugins/contextmenu.js里边的默认菜单的例子，label留空支持国际化，否则以此配置为准
        //,contextMenu:[
        //    {
        //        label:'',       //显示的名称
        //        cmdName:'selectall',//执行的command命令，当点击这个右键菜单时
        //        //exec可选，有了exec就会在点击时执行这个function，优先级高于cmdName
        //        exec:function () {
        //            //this是当前编辑器的实例
        //            //this.ui._dialogs['inserttableDialog'].open();
        //        }
        //    }
        //]

        //快捷菜单
        //,shortcutMenu:["fontfamily", "fontsize", "bold", "italic", "underline", "forecolor", "backcolor", "insertorderedlist", "insertunorderedlist"]

        //elementPathEnabled
        //是否启用元素路径，默认是显示
        //,elementPathEnabled : true

        //wordCount
        //,wordCount:true          //是否开启字数统计
        //,maximumWords:10000       //允许的最大字符数
        //字数统计提示，{#count}代表当前字数，{#leave}代表还可以输入多少字符数,留空支持多语言自动切换，否则按此配置显示
        //,wordCountMsg:''   //当前已输入 {#count} 个字符，您还可以输入{#leave} 个字符
        //超出字数限制提示  留空支持多语言自动切换，否则按此配置显示
        //,wordOverFlowMsg:''    //<span style="color:red;">你输入的字符个数已经超出最大允许值，服务器可能会拒绝保存！</span>

        //tab
        //点击tab键时移动的距离,tabSize倍数，tabNode什么字符做为单位
        //,tabSize:4
        //,tabNode:'&nbsp;'

        //removeFormat
        //清除格式时可以删除的标签和属性
        //removeForamtTags标签
        //,removeFormatTags:'b,big,code,del,dfn,em,font,i,ins,kbd,q,samp,small,span,strike,strong,sub,sup,tt,u,var'
        //removeFormatAttributes属性
        //,removeFormatAttributes:'class,style,lang,width,height,align,hspace,valign'

        //undo
        //可以最多回退的次数,默认20
        //,maxUndoCount:20
        //当输入的字符数超过该值时，保存一次现场
        //,maxInputCount:1

        //autoHeightEnabled
        // 是否自动长高,默认true
        //,autoHeightEnabled:true

        //scaleEnabled
        //是否可以拉伸长高,默认true(当开启时，自动长高失效)
        //,scaleEnabled:false
        //,minFrameWidth:800    //编辑器拖动时最小宽度,默认800
        //,minFrameHeight:220  //编辑器拖动时最小高度,默认220

        //autoFloatEnabled
        //是否保持toolbar的位置不动,默认true
        //,autoFloatEnabled:true
        //浮动时工具栏距离浏览器顶部的高度，用于某些具有固定头部的页面
        //,topOffset:30
        //编辑器底部距离工具栏高度(如果参数大于等于编辑器高度，则设置无效)
        //,toolbarTopOffset:400

        //设置远程图片是否抓取到本地保存
        //,catchRemoteImageEnable: true //设置是否抓取远程图片

        //pageBreakTag
        //分页标识符,默认是_ueditor_page_break_tag_
        //,pageBreakTag:'_ueditor_page_break_tag_'

        //autotypeset
        //自动排版参数
        //,autotypeset: {
        //    mergeEmptyline: true,           //合并空行
        //    removeClass: true,              //去掉冗余的class
        //    removeEmptyline: false,         //去掉空行
        //    textAlign:"left",               //段落的排版方式，可以是 left,right,center,justify 去掉这个属性表示不执行排版
        //    imageBlockLine: 'center',       //图片的浮动方式，独占一行剧中,左右浮动，默认: center,left,right,none 去掉这个属性表示不执行排版
        //    pasteFilter: false,             //根据规则过滤没事粘贴进来的内容
        //    clearFontSize: false,           //去掉所有的内嵌字号，使用编辑器默认的字号
        //    clearFontFamily: false,         //去掉所有的内嵌字体，使用编辑器默认的字体
        //    removeEmptyNode: false,         // 去掉空节点
        //    //可以去掉的标签
        //    removeTagNames: {标签名字:1},
        //    indent: false,                  // 行首缩进
        //    indentValue : '2em',            //行首缩进的大小
        //    bdc2sb: false,
        //    tobdc: false
        //}

        //tableDragable
        //表格是否可以拖拽
        //,tableDragable: true



        //sourceEditor
        //源码的查看方式,codemirror 是代码高亮，textarea是文本框,默认是codemirror
        //注意默认codemirror只能在ie8+和非ie中使用
        //,sourceEditor:"codemirror"
        //如果sourceEditor是codemirror，还用配置一下两个参数
        //codeMirrorJsUrl js加载的路径，默认是 URL + "third-party/codemirror/codemirror.js"
        //,codeMirrorJsUrl:URL + "third-party/codemirror/codemirror.js"
        //codeMirrorCssUrl css加载的路径，默认是 URL + "third-party/codemirror/codemirror.css"
        //,codeMirrorCssUrl:URL + "third-party/codemirror/codemirror.css"
        //编辑器初始化完成后是否进入源码模式，默认为否。
        //,sourceEditorFirst:false

        //iframeUrlMap
        //dialog内容的路径 ～会被替换成URL,垓属性一旦打开，将覆盖所有的dialog的默认路径
        //,iframeUrlMap:{
        //    'anchor':'~/dialogs/anchor/anchor.html',
        //}

        //allowLinkProtocol 允许的链接地址，有这些前缀的链接地址不会自动添加http
        //, allowLinkProtocols: ['http:', 'https:', '#', '/', 'ftp:', 'mailto:', 'tel:', 'git:', 'svn:']

        //webAppKey 百度应用的APIkey，每个站长必须首先去百度官网注册一个key后方能正常使用app功能，注册介绍，http://app.baidu.com/static/cms/getapikey.html
        //, webAppKey: ""

        //默认过滤规则相关配置项目
        //,disabledTableInTable:true  //禁止表格嵌套
        //,allowDivTransToP:true      //允许进入编辑器的div标签自动变成p标签
        //,rgb2Hex:true               //默认产出的数据中的color自动从rgb格式变成16进制格式

        // xss 过滤是否开启,inserthtml等操作
		, xssFilterRules: true
        //input xss过滤
		, inputXssFilter: true
        //output xss过滤
		, outputXssFilter: true
        // xss过滤白名单 名单来源: https://raw.githubusercontent.com/leizongmin/js-xss/master/lib/default.js
		, whitList: {
		    a: ['target', 'href', 'title', 'class', 'style'],
		    abbr: ['title', 'class', 'style'],
		    address: ['class', 'style'],
		    area: ['shape', 'coords', 'href', 'alt'],
		    article: [],
		    aside: [],
		    audio: ['autoplay', 'controls', 'loop', 'preload', 'src', 'class', 'style'],
		    b: ['class', 'style'],
		    bdi: ['dir'],
		    bdo: ['dir'],
		    big: [],
		    blockquote: ['cite', 'class', 'style'],
		    br: [],
		    caption: ['class', 'style'],
		    center: [],
		    cite: [],
		    code: ['class', 'style'],
		    col: ['align', 'valign', 'span', 'width', 'class', 'style'],
		    colgroup: ['align', 'valign', 'span', 'width', 'class', 'style'],
		    dd: ['class', 'style'],
		    del: ['datetime'],
		    details: ['open'],
		    div: ['class', 'style'],
		    dl: ['class', 'style'],
		    dt: ['class', 'style'],
		    em: ['class', 'style'],
		    font: ['color', 'size', 'face'],
		    footer: [],
		    h1: ['class', 'style'],
		    h2: ['class', 'style'],
		    h3: ['class', 'style'],
		    h4: ['class', 'style'],
		    h5: ['class', 'style'],
		    h6: ['class', 'style'],
		    header: [],
		    hr: [],
		    i: ['class', 'style'],
		    img: ['src', 'alt', 'title', 'width', 'height', 'id', '_src', 'loadingclass', 'class', 'data-latex'],
		    ins: ['datetime'],
		    li: ['class', 'style'],
		    mark: [],
		    nav: [],
		    ol: ['class', 'style'],
		    p: ['class', 'style'],
		    pre: ['class', 'style'],
		    s: [],
		    section: [],
		    small: [],
		    span: ['class', 'style'],
		    sub: ['class', 'style'],
		    sup: ['class', 'style'],
		    strong: ['class', 'style'],
		    table: ['width', 'border', 'align', 'valign', 'class', 'style'],
		    tbody: ['align', 'valign', 'class', 'style'],
		    td: ['width', 'rowspan', 'colspan', 'align', 'valign', 'class', 'style'],
		    tfoot: ['align', 'valign', 'class', 'style'],
		    th: ['width', 'rowspan', 'colspan', 'align', 'valign', 'class', 'style'],
		    thead: ['align', 'valign', 'class', 'style'],
		    tr: ['rowspan', 'align', 'valign', 'class', 'style'],
		    tt: [],
		    u: [],
		    ul: ['class', 'style'],
		    video: ['autoplay', 'controls', 'loop', 'preload', 'src', 'height', 'width', 'class', 'style']
		}
    };

})(jQuery);
