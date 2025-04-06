<template>
  <div class="container mt-4">
    <div><slot name="createbutton" /></div>
    <table class="table table-bordered">
      <thead>
        <tr>
          <th scope="col" class="fixed-width-col">To Do</th>
          <th scope="col" class="fixed-width-col">In progress</th>
          <th scope="col" class="fixed-width-col">Done</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>
            <slot name="todo" />
          </td>
          <td><slot name="inProgress" /></td>
          <td><slot name="done" /></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style lang="css">
.fixed-width-col {
  width: 33%;
}
</style>
