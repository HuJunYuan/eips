
/**
    定义统一交json数据交互结构 
    { 
          Success         是否成功
        , Message         消息 返回数据时附带的消息， 一般可能用不到
        , Icon            信息的Icon标识（0:警告 1:提示信息 2:错误信息 3:提问信息）
        , CallBackScript  消息显示后的回调函数
        , data            实际需要的数据 任意类型
    }
*/
(function ($, win) {

    if (win.App) return;

    var App = window.App = {};

    var boxIndex = 0;

    $.ajaxSetup({
        type: "POST",
        cache: false,
        dataType: 'json',
        contentType: "application/json",
        dataFilter: function (responseText, type) {
            return analysisMessage(responseText, type);
        },
        error: function (res) { analysisMessage(res.data, res.dataType); }
    });
    $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
        analysisMessage(jqxhr.responseText, settings.dataType);
    });
 

    function analysisMessage(responseText, type) {
        if (type == "json") {
            var data = JSON.parse(responseText);
            // 扩展后台输出的提示消息，显示消息后将消息数据过滤掉
            if (data.Success != undefined) {
                App.showMessageBox(data);
                data = data.Data;
            }
            responseText = JSON.stringify(data);
        }
        return responseText;
    }

    App.rootPath = (function () {
        return $.util.getRootPath() + '/';
    })();

    App.getUrl = function (path) {
        if (path.indexOf('http://') == 0 || path.indexOf('https://') == 0)
            return path;

        if (path.indexOf("/") != 0)
            path = App.rootPath + path;
        return path || "/";
    }

    App.ajax = function (url, options) {
        $.ajax(url, options);
    }

    App.action = function (url, data /*, options, fn_success, fn_error*/) {
        var options = null, fn_success = null, fn_error = null;
        if (arguments.length == 3) {
            fn_success = arguments[2];
        }
        else if (arguments.length == 4) {
            if (typeof arguments[2] === "function") {
                fn_success = arguments[2];
                fn_error = arguments[3];
            }
            else {
                options = arguments[2];
                fn_success = arguments[3];
            }
        }
        else {
            options = arguments[2];
            fn_success = arguments[3];
            fn_error = arguments[4];
        }

        url = App.getUrl(url);
        options = $.extend({
            url: url,
            data: JSON.stringify(data),
            success: function (result) {
                // 关闭Loading
                App.closeLayer();
                if (fn_success) fn_success(result);
            },
            error: function (e) {
                // 关闭Loading
                App.closeLayer();
                if (fn_error) fn_error(e);
                // 系统处理
            }
        }, options);

        App.showLoading();
        App.ajax(url, options);
    }

    App.get = function (url, data, fn_success, fn_error) {
        url = App.getUrl(url);
        var options = {
            type: "GET",
            url: url,
            data: data,
            success: function (result) {
                // 关闭Loading
                App.closeLayer();
                if (fn_success) fn_success(result);
            },
            error: function () {
                // 关闭Loading
                App.closeLayer();
                if (fn_error) fn_error();
            }
        };
        App.showLoading();
        App.ajax(url, options);
    }

    App.getString = function (url, fu_success) {
        url = App.getUrl(url);
        var options = {
            url: url,
            data: '[]',
            dataType: "html",
            contentType: "application/x-www-form-urlencoded",
            Accept: "text/html",
            success: function (result) {
                if (fn_success) fn_success(result);
            },
            error: function (e) {
                alert(e);
            }
        };

        App.ajax(url, options);
    }

    App.showMessageBox = function (msg, icon) {
        //信息的Icon标识（0:警告信息 1:提示信息 2:错误信息 3:提问信息）
        var icons = { 0: 'warning', 1: 'info', 2: 'error', 3: 'question' };
        var titles = { 0: '警告信息', 1: '提示信息', 2: '错误信息', 3: '提问信息' };

        if ($.util.isString(msg)) {
            $.messager.alert(titles[icon], msg, icons[icon]);
        } else if (msg.Message && msg.Message != "") {
            $.messager.alert(titles[msg.Icon], msg.Message, icons[msg.Icon], function () {
                var callBack = msg.CallBackScript;
                eval(callBack);
            });
        }
    }

    App.showLoading = function () {
        $.messager.progress();
    }

    //显示确认对话框
    App.showConfirm = function (msg) {

        var fn_ok = null, fn_cancel = null;

        if (arguments.length == 2) {
            fn_ok = arguments[1];
        } else {
            fn_ok = arguments[1];
            fn_cancel = arguments[2];
        }

        $.messager.confirm('提问信息', msg, function (val) {

            if (val && fn_ok) {
                fn_ok();
            } else if (!val && fn_cancel) {
                fn_cancel();
            }
        });
    }

    //关闭弹出的层
    App.closeLayer = function () {
        $.messager.progress('close');
    }

    //显示一个带"确定"和"取消"按钮的消息窗口，提示用户输入一些文本。参数：
    //msg：要显示的消息文本。
    //fn(val)：带有用户输入的数值参数的回调函数。    
    App.showPrompt = function (msg,fn) {
        $.messager.prompt('提示信息', msg, function (val) {
            if (fn && val != undefined) {
                fn(val);
            }
        });
    }

    App.openwindow = function (url, name, width, height, resizable) {
        var iWidth = arguments.length > 2 ? width : window.screen.availWidth - 100;                          //弹出窗口的宽度;
        var iHeight = arguments.length > 3 ? height : window.screen.availHeight - 50;                        //弹出窗口的高度;
        var oppResizable = arguments.length > 4 ? resizable : 'yes';                        //大小是否可调
        var iTop = (window.screen.availHeight - 30 - iHeight) / 2;       //获得窗口的垂直位置;
        var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;           //获得窗口的水平位置;
        name = name ? name : Math.floor(Math.random() * 1000);
        window.open(url, name, 'height=' + iHeight + ',innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=yes,resizable=' + oppResizable + ',location=no,status=no,titlebar=no,z-look=yes');
    }

    //对使用 window.open(） 的页面，刷新父级页面，并关闭当前页面
    App.RefreshOpenerWindow = function () {
        window.opener.location.href = window.opener.location.href;
        window.close();
    }

    //对使用 window.open(） 的页面，关闭当前页面
    App.CloseWindow = function () {
        window.close();
    }
    // 清除iframe
    App.clearIframe = function(el) {
        var iframe = el.contentWindow;
        if (el) {
            el.src = 'about:blank';
            try {
                iframe.document.write('');
                iframe.document.clear();
                //以上可以清除大部分的内存和文档节点记录数了
                //最后删除掉这个 iframe 就哦咧。
                document.body.removeChild(el);
            } catch (e) { };
        }
    }

    App.GetDIT = function (ditID, empID, nodeType, srBoxIds) {
        var arrDefSelNode = [];
        if (empID != "-1") {

            var ids = empID.split(',');
            for (var i = 0; i < ids.length; i++) {
                if (ids[i] != '') {
                    var objN = {
                        'id': ids[i], 'nodeType': nodeType, 'srBoxIds': srBoxIds
                    };
                    arrDefSelNode.push(objN);
                }
            }
        }

        var dit = new DIT({
            'ditId': ditID, 'ditType': 'TreeWithSRB', 'employeeID': empID, 'baseURL': 'http://' + window.location.host + '/uuc_dit/', 'selectedNodes': arrDefSelNode
        });
        dit.open();
    }


    //显示用户的名片
    App.ShowUserCard = function () {
        App.openwindow(url, '', 510, 485);
    }

    //阅读日志
    /*
    appId 应用程序ID
    requestUrl 请求接口URL
    url 当前页面URL
    articleId 当前页面文章编号
    */
    App.VisitInit = function (appId, requestUrl, url) {
        //阅读日志相关处理
        var visitInitObj = {
            requestInterface: 'GetUrlPeopleRecord',
            appId: appId,
            requestUrl: requestUrl, //http://eipsps.scmcc.com.cn/NewEipUniStatistic 请求URL，保持不变  生产环境是http://eipapp.scmcc.com.cn/NewEipUniStatistic
            url: url,
            filterParams: [""],
            isRequiredParams: false,
            alias: '',
            visitSuccessProcess: function (res) {
                var visitCount = res.Count;
                var visitorCount = res.PeopleCount;
                var commentCount = res.CommentCount;

            }
        };

        window.EIPUnitVisit.init(visitInitObj);
    }


})(jQuery, window);

if (!window.console) window.console = { log: function () { } };