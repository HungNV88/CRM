(function(n){function i(n,t){for(var i=window,r=(n||"").split(".");i&&r.length;)i=i[r.shift()];return typeof i=="function"?i:(t.push(n),Function.constructor.apply(null,t))}function u(n){return n==="GET"||n==="POST"}function o(n,t){!u(t)&&n.setRequestHeader("X-HTTP-Method-Override",t)}function s(t,i,r){var u;r.indexOf("application/x-javascript")===-1&&(u=(t.getAttribute("data-ajax-mode")||"").toUpperCase(),n(t.getAttribute("data-ajax-update")).each(function(t,r){var f;switch(u){case"BEFORE":f=r.firstChild,n("<div />").html(i).contents().each(function(){r.insertBefore(this,f)});break;case"AFTER":n("<div />").html(i).contents().each(function(){r.appendChild(this)});break;default:n(r).html(i)}}))}function r(t,r){var h,c,f,e;(h=t.getAttribute("data-ajax-confirm"),!h||window.confirm(h))&&(c=n(t.getAttribute("data-ajax-loading")),e=t.getAttribute("data-ajax-loading-duration")||0,n.extend(r,{type:t.getAttribute("data-ajax-method")||undefined,url:t.getAttribute("data-ajax-url")||undefined,beforeSend:function(n){var r;return o(n,f),r=i(t.getAttribute("data-ajax-begin"),["xhr"]).apply(this,arguments),r!==!1&&c.show(e),r},complete:function(){c.hide(e),i(t.getAttribute("data-ajax-complete"),["xhr","status"]).apply(this,arguments)},success:function(n,r,u){s(t,n,u.getResponseHeader("Content-Type")||"text/html"),i(t.getAttribute("data-ajax-success"),["data","status","xhr"]).apply(this,arguments)},error:i(t.getAttribute("data-ajax-failure"),["xhr","status","error"])}),r.data.push({name:"X-Requested-With",value:"XMLHttpRequest"}),f=r.type.toUpperCase(),u(f)||(r.type="POST",r.data.push({name:"X-HTTP-Method-Override",value:f})),n.ajax(r))}function e(t){var i=n(t).data(f);return!i||!i.validate||i.validate()}var t="unobtrusiveAjaxClick",f="unobtrusiveValidation";n("a[data-ajax=true]").live("click",function(n){n.preventDefault(),r(this,{url:this.href,type:"GET",data:[]})}),n("form[data-ajax=true] input[type=image]").live("click",function(i){var f=i.target.name,e=n(i.target),r=e.parents("form")[0],u=e.offset();n(r).data(t,[{name:f+".x",value:Math.round(i.pageX-u.left)},{name:f+".y",value:Math.round(i.pageY-u.top)}]),setTimeout(function(){n(r).removeData(t)},0)}),n("form[data-ajax=true] :submit").live("click",function(i){var u=i.target.name,r=n(i.target).parents("form")[0];n(r).data(t,u?[{name:u,value:i.target.value}]:[]),setTimeout(function(){n(r).removeData(t)},0)}),n("form[data-ajax=true]").live("submit",function(i){var u=n(this).data(t)||[];(i.preventDefault(),e(this))&&r(this,{url:this.action,type:this.method||"GET",data:u.concat(n(this).serializeArray())})})})(jQuery)