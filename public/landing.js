(function(){
  var header=document.getElementById('siteHeader');
  function onScroll(){header.classList.toggle('is-scrolled',window.scrollY>8)}
  window.addEventListener('scroll',onScroll,{passive:true});onScroll();

  var toggle=document.getElementById('navToggle'),nav=document.getElementById('mobileNav');
  toggle.addEventListener('click',function(){
    var open=nav.classList.toggle('open');
    toggle.setAttribute('aria-expanded',open?'true':'false');
    toggle.textContent=open?'✕':'☰';
  });
  nav.querySelectorAll('a').forEach(function(a){a.addEventListener('click',function(){nav.classList.remove('open');toggle.setAttribute('aria-expanded','false');toggle.textContent='☰'})});

  // Reveal on scroll
  var reduce=window.matchMedia('(prefers-reduced-motion: reduce)').matches;
  var els=document.querySelectorAll('.reveal');
  if(reduce||!('IntersectionObserver' in window)){els.forEach(function(e){e.classList.add('is-visible')});}
  else{
    var io=new IntersectionObserver(function(entries){
      entries.forEach(function(en){if(en.isIntersecting){en.target.classList.add('is-visible');io.unobserve(en.target)}});
    },{threshold:.12,rootMargin:'0px 0px -40px 0px'});
    els.forEach(function(e){io.observe(e)});
  }

  // FAQ accordion (single-open)
  var items=document.querySelectorAll('.faq-item');
  items.forEach(function(item){
    var btn=item.querySelector('.faq-q'),ans=item.querySelector('.faq-a');
    btn.addEventListener('click',function(){
      var isOpen=item.classList.contains('open');
      items.forEach(function(o){o.classList.remove('open');o.querySelector('.faq-a').style.maxHeight=null;o.querySelector('.faq-q').setAttribute('aria-expanded','false')});
      if(!isOpen){item.classList.add('open');ans.style.maxHeight=ans.scrollHeight+'px';btn.setAttribute('aria-expanded','true')}
    });
  });

  // Hero live demo: cycle barcode values
  var codes=['890123456789','899276311045','978602033207','210987654321'];
  var targets=['Terketik ke Stok.xlsx + Enter ⏎','Terketik ke Kasir POS + Enter ⏎','Terketik ke Notepad + Tab ⇥','Terketik ke Stok.xlsx + Enter ⏎'];
  var i=0,tc=document.getElementById('toastCode'),tt=document.getElementById('toastTarget');
  if(!reduce&&tc){setInterval(function(){i=(i+1)%codes.length;tc.textContent=codes[i];tt.textContent=targets[i]},2600)}
})();
