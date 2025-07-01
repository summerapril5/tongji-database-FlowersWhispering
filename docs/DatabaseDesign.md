<h1><center>数据库设计文档</center></h1>

## 实体和关系描述

### 实体列表：

- **Users**
- **Plants**
- **Comments**
- **Messages**
- **Favorites**
- **Articles**
- **Reviews**
- **UserPoints**
- **UserGroups**
- **CareLogs**
- **Feedbacks**
- **PlantImages**
- **PlantRegionSuitability**
- **UserActivityLogs**
- **CareReminders**
- **MedicinalHerbs**

### 关系描述

- **Users** 与 **Comments**：一对多关系（一个用户可以发表多个评论）
- **Plants** 与 **Comments**：一对多关系（一个植物可以有多个评论）
- **Users** 与 **Messages**：自引用多对多关系（用户之间可以相互发送消息）
- **Users** 与 **Favorites**：一对多关系（一个用户可以收藏多个植物）
- **Plants** 与 **Favorites**：一对多关系（一个植物可以被多个用户收藏）
- **Users** 与 **Articles**：一对多关系（一个用户可以发布多篇文章或公告）
- **Users** 与 **Reviews**：一对多关系（一个用户可以提交多个审核建议）
- **Plants** 与 **Reviews**：一对多关系（一个植物可以有多个审核建议）
- **Users** 与 **UserPoints**：一对一关系（每个用户有一个积分记录）
- **Users** 与 **UserGroups**：多对多关系（用户可以属于多个小组）
- **Users** 与 **CareLogs**：一对多关系（一个用户可以有多个养护日志）
- **Plants** 与 **CareLogs**：一对多关系（一个植物可以有多个养护日志）
- **Users** 与 **Feedbacks**：一对多关系（一个用户可以提交多个反馈）
- **Plants** 与 **PlantImages**：一对多关系（一个植物可以有多张图片）
- **Plants** 与 **PlantRegionSuitability**：一对多关系（一个植物可以适应多个地区）
- **Plants** 与 **MedicinalHerbs**：一对一关系（每种药用植物对应一个植物记录）



## 表设计

### 1. 用户表（Users）

- `用户ID` **user_id** (NUMBER, PK) - 唯一的用户标识。
- `用户名` **username** (VARCHAR2(50)) - 用户的用户名，唯一。
- `用户密码` **password** (VARCHAR2(50)) - 用户的密码。
- ` 邮箱`**email** (VARCHAR2(100)) - 用户的电子邮箱，唯一。
- `注册日期` **registration_date** (DATE) - 用户注册的日期。
- `角色` **user_role**(VARCHAR2(20)) - 用户的身份(如管理员、普通用户等)
- `用户状态` **user_status** (VARCHAR2(20)) - 用户的账户状态（如活跃、封禁）。
- `多语言偏好` **language_preference** (VARCHAR2(20)) - 用户的语言偏好。

### 2. 植物表（Plants）
- `植物ID` **plant_id** (NUMBER, PK) - 唯一的植物标识。
- `常见名称` **common_name** (VARCHAR2(100)) - 植物的常见名称。
- `学名` **scientific_name** (VARCHAR2(100)) - 植物的学名。
- `分类` **category** (VARCHAR2(100)) - 植物的分类。
- `描述` **portrayal** (CLOB) - 植物的详细描述。
- `生长环境` **growth_environment** (VARCHAR2(255)) - 植物的生长环境。
- `养护条件` **care_conditions** (CLOB) - 植物的养护条件。

### 3. 评论表（Comments）
- `评论ID` **comment_id** (NUMBER, PK) - 唯一的评论标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 评论者的用户ID。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 评论的植物ID。
- `评论内容` **comment_content** (CLOB) - 评论的内容。
- `创建时间` **created_date** (DATE) - 评论创建的时间。

### 4. 私信表（Messages）
- `消息ID` **message_id** (NUMBER, PK) - 唯一的消息标识。
- `发送者ID` **sender_id** (NUMBER, FK -> Users.user_id) - 消息发送者的用户ID。
- `接收者ID` **receiver_id** (NUMBER, FK -> Users.user_id) - 消息接收者的用户ID。
- `消息内容` **message_content** (CLOB) - 消息的内容。
- `发送时间` **sent_date** (DATE) - 消息发送的时间。

### 5. 收藏表（Favorites）
- `收藏ID` **favorite_id** (NUMBER, PK) - 唯一的收藏标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 收藏者的用户ID。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 被收藏的植物ID。

### 6. 文章和公告表（Articles）
- `文章ID` **article_id** (NUMBER, PK) - 唯一的文章或公告标识。
- `标题` **article_title** (VARCHAR2(200)) - 文章或公告的标题。
- `内容` **article_content** (CLOB) - 文章或公告的内容。
- `发布者ID` **publisher_id** (NUMBER, FK -> Users.user_id) - 发布者的用户ID。
- `发布日期` **published_date** (DATE) - 发布的日期。
- `类型（文章或公告）` **article_type** (VARCHAR2(50)) - 文章类型，区分是文章还是公告。

### 7. 审核表（Reviews）
- `审核ID` **review_id** (NUMBER, PK) - 唯一的审核标识。
- `修改内容` **modified_content** (CLOB) - 修改的内容描述。
- `提交日期` **submitted_date** (DATE) - 提交的日期。
- `提交者ID` **submitter_id** (NUMBER, FK -> Users.user_id) - 提交审核的用户ID。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 被审核的植物ID。
- `审核状态` **review_status** (VARCHAR2(20)) - 审核的状态。
- `审核日期` **review_date** (DATE) - 审核的日期。

### 8. 用户积分表（UserPoints）
- `积分ID` **points_id** (NUMBER, PK) - 唯一的积分标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 用户ID。
- `积分` **points** (NUMBER) - 积分数。
- `最后更新时间` **last_updated** (DATE) - 最后更新时间。

### 9. 用户组表（UserGroups）
- `组ID` **group_id** (NUMBER, PK) - 唯一的用户组标识。

- `组名` **group_name** (VARCHAR2(100)) - 用户组的名称。
- `创建者ID` **creator_id** (NUMBER, FK -> Users.user_id) - 创建者的用户ID。
- `创建时间` **created_date** (DATE) - 创建日期。

### 10. 养护日志表（CareLogs）
- `日志ID` **log_id** (NUMBER, PK) - 唯一的日志标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 记录者的用户ID。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 相关的植物ID。
- `养护活动` **care_activity** (VARCHAR2(255)) - 养护活动描述。
- `日志日期` **log_date** (DATE) - 日志日期。

### 11. 用户反馈表（Feedbacks）
- `反馈ID` **feedback_id** (NUMBER, PK) - 唯一的反馈标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 提交反馈的用户ID。
- `反馈内容` **feedback_content** (CLOB) - 反馈内容。
- `提交日期` **submitted_date** (DATE) - 提交日期。

### 12. 植物图片表（PlantImages）
- `图片ID` **image_id** (NUMBER, PK) - 唯一的图片标识。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 相关植物的ID。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id, optional) - 上传者的用户ID。
- `图片URL` **image_url** (VARCHAR2(255)) - 图片的URL。
- `识别结果` **recognition_result** (CLOB) - 图片识别结果。
- `诊断信息（可选）` **diagnosis_result** (CLOB, optional) - 诊断结果（如果适用）。
- `上传日期` **upload_date** (DATE) - 上传日期。

### 13. 地区适宜性表（PlantRegionSuitability）
- `适宜性ID` **suitability_id** (NUMBER, PK) - 唯一的适宜性标识。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 植物ID。
- `地区` **region_name** (VARCHAR2(100)) - 地区名称。
- `适宜性描述` **suitability** (VARCHAR2(255)) - 适宜性描述。

### 14. 用户活动日志表（UserActivityLogs）
- `日志ID` **log_id** (NUMBER, PK) - 唯一的日志标识。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 用户ID。
- `活动类型` **activity_type** (VARCHAR2(100)) - 活动类型。
- `活动描述` **activity_description** (CLOB) - 活动描述。
- `活动日期` **activity_date** (DATE) - 活动日期。

### 15. 养护提醒服务表（CareReminders）
- `提醒ID` **reminder_id** (NUMBER, PK) - 唯一的提醒标识。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 植物ID。
- `用户ID` **user_id** (NUMBER, FK -> Users.user_id) - 用户ID。
- `提醒文本` **reminder_text** (VARCHAR2(255)) - 提醒内容。
- `提醒日期` **reminder_date** (DATE) - 提醒日期。
- `是否启用` **is_active** (CHAR(1)) - 是否启用提醒（'Y' 或 'N'）。

### 16. 中草药表（MedicinalHerbs）
- `草药ID` **herb_id** (NUMBER, PK) - 唯一的药草标识。
- `植物ID` **plant_id** (NUMBER, FK -> Plants.plant_id) - 关联植物ID。
- `药用属性` **medicinal_properties** (CLOB) - 药用属性。
- `使用说明` **usage_instructions** (CLOB) - 使用说明。

这些表和字段的设计确保了数据库不仅可以存储所有必要的信息，而且还能维护数据之间的关系和完整性。

## SQL语言



### 创建用户表

```sql
CREATE TABLE Users (
    user_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    username VARCHAR2(50) NOT NULL UNIQUE,
    password VARCHAR2(50) NOT NULL,
    email VARCHAR2(100) NOT NULL UNIQUE,
    registration_date DATE DEFAULT SYSDATE,
    status VARCHAR2(20) DEFAULT 'active',
    role VARCHAR2(20) DEFAULT 'user',
    language_preference VARCHAR2(20) DEFAULT 'chinese'
);
```

### 创建植物表

```sql
CREATE TABLE Plants (
    plant_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    common_name VARCHAR2(100),
    scientific_name VARCHAR2(100),
    classification VARCHAR2(100),
    description CLOB,
    growth_environment VARCHAR2(255),
    care_conditions CLOB
);
```

### 创建评论表

```sql
CREATE TABLE Comments (
    comment_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    plant_id NUMBER REFERENCES Plants(plant_id),
    comment_content CLOB,
    created_date DATE DEFAULT SYSDATE
);
```

### 创建私信表

```sql
CREATE TABLE Messages (
    message_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    sender_id NUMBER REFERENCES Users(user_id),
    receiver_id NUMBER REFERENCES Users(user_id),
    message_content CLOB,
    sent_date DATE DEFAULT SYSDATE
);
```

### 创建收藏表

```sql
CREATE TABLE Favorites (
    favorite_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    plant_id NUMBER REFERENCES Plants(plant_id)
);
```

### 创建文章和公告表

```sql
CREATE TABLE Articles (
    article_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    article_title VARCHAR2(200),
    article_content CLOB,
    publisher_id NUMBER REFERENCES Users(user_id),
    published_date DATE DEFAULT SYSDATE,
    article_type VARCHAR2(50) DEFAULT 'article'  -- 'article' or 'announcement'
);
```

### 创建审核表

```sql
CREATE TABLE Reviews (
    review_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    submitter_id NUMBER REFERENCES Users(user_id),
    modified_content CLOB,
    review_status VARCHAR2(20) DEFAULT 'pending',
    submitted_date DATE DEFAULT SYSDATE,
    review_date DATE
);
```

### 创建用户积分表

```sql
CREATE TABLE UserPoints (
    points_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    points NUMBER DEFAULT 0,
    last_updated DATE DEFAULT SYSDATE
);
```

### 创建用户组表

```sql
CREATE TABLE UserGroups (
    group_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    group_name VARCHAR2(100),
    creator_id NUMBER REFERENCES Users(user_id),
    created_date DATE DEFAULT SYSDATE
);
```

### 创建养护日志表

```sql
CREATE TABLE CareLogs (
    log_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    plant_id NUMBER REFERENCES Plants(plant_id),
    care_activity CLOB,
    log_date DATE DEFAULT SYSDATE
);
```

### 创建用户反馈表

```sql
CREATE TABLE Feedbacks (
    feedback_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    feedback_content CLOB,
    submitted_date DATE DEFAULT SYSDATE
);
```

### 创建植物图片表

```sql
CREATE TABLE PlantImages (
    image_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    user_id NUMBER REFERENCES Users(user_id),
    image_url VARCHAR2(255),
    recognition_result CLOB,
    diagnosis_result CLOB,
    upload_date DATE DEFAULT SYSDATE
);
```

### 创建地区适宜性表

```sql
CREATE TABLE PlantRegionSuitability (
    suitability_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    region_name VARCHAR2(100),
    suitability VARCHAR2(255)
);
```

### 创建用户活动日志表

```sql
CREATE TABLE UserActivityLogs (
    log_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    user_id NUMBER REFERENCES Users(user_id),
    activity_type VARCHAR2(100),
    activity_description CLOB,
    activity_date DATE DEFAULT SYSDATE
);
```

### 创建养护提醒服务表

```sql
CREATE TABLE CareReminders (
    reminder_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    user_id NUMBER REFERENCES Users(user_id),
    reminder_text VARCHAR2(255),
    reminder_date DATE,
    is_active CHAR(1) DEFAULT 'Y'
);
```

### 创建中草药表

```sql
CREATE TABLE MedicinalHerbs (
    herb_id NUMBER GENERATED BY DEFAULT AS IDENTITY START WITH 1 PRIMARY KEY,
    plant_id NUMBER REFERENCES Plants(plant_id),
    medicinal_properties CLOB,
    usage_instructions CLOB
);
```