''' 这是一个将excel表格转为json的工具脚本，建议保存到本地修改该脚本，再把转好的文件放在项目里 '''

import json
# 如果相对路径不行就写绝对路径，再不行就把它们放到一个python项目里
# 1.这里写表格文件名（.xlsx/.xls/.csv）
f=open("stateInfo.csv","r",encoding='gb2312')
ls=[]
for line in f:
        line = line.replace("\n", "")
        ls.append(line.split(","))

f.close()
# 2.这里写导出文件名，注意修改列名
fw=open("stateInfo.json","w",encoding='utf-8')
for i in range(1,len(ls)):
    ls[i]=dict(zip(ls[0],ls[i]))
a = json.dumps(ls[1:],sort_keys=True,indent=4,ensure_ascii=False)
print(a)
fw.write(a)
fw.close()
